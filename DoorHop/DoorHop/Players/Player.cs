using DoorHop.Animation;
using DoorHop.Input;
using DoorHop.Interfaces;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace DoorHop.Players
{
    public abstract class Player : IGameObject
    {
        protected Texture2D playerTexture;
        protected Animatie currentAnimation;
        protected Animatie runAnimation;
        protected Animatie idleAnimation;
        protected Vector2 position;
        protected Vector2 velocity;
        protected IInputReader inputReader;
        protected float moveSpeed = 5f;
        protected float jumpForce = -12f;
        protected bool isMoving;
        protected bool isJumping;
        protected bool isGrounded;
        protected bool isFacingRight = true;
        protected Rectangle bounds;
        
        protected const float gravity = 0.6f;
        protected const float maxFallSpeed = 12f;
        protected const int COLLISION_WIDTH = 48;
        protected const int COLLISION_HEIGHT = 48;

        protected Player(ContentManager content, IInputReader inputReader)
        {
            this.inputReader = inputReader;
            position = new Vector2(200, 200);
            velocity = Vector2.Zero;
            moveSpeed = 5f;
            isJumping = false;
            isMoving = false;
            isGrounded = false;
        }

        protected abstract void LoadContent(ContentManager content);

        public virtual void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            Vector2 input = inputReader.ReadInput();
            velocity.X = input.X * moveSpeed;
            
            if (input.X < 0) isFacingRight = false;
            else if (input.X > 0) isFacingRight = true;
            
            isMoving = Math.Abs(velocity.X) > 0;

            // Spring logica
            if (inputReader.IsJumpKeyPressed() && isGrounded)
            {
                velocity.Y = jumpForce;
                isJumping = true;
                isGrounded = false;
            }

            // Zwaartekracht
            if (!isGrounded)
            {
                velocity.Y = Math.Min(velocity.Y + gravity, maxFallSpeed);
            }

            // Handle collisions (beweging gebeurt nu in HandleCollisions)
            HandleCollisions(tiles);
            UpdateAnimation(gameTime);
        }

        protected virtual void HandleCollisions(List<TileMap.CollisionTiles> tiles)
        {
            isGrounded = false;

            // Sla de originele positie op voor horizontale beweging
            float originalX = position.X;
            position.X += velocity.X;
            UpdateBounds();

            foreach (var tile in tiles)
            {
                if (bounds.Intersects(tile.Rectangle))
                {
                    // Herstel positie bij collision
                    position.X = originalX;
                    velocity.X = 0;
                    UpdateBounds();
                    break;
                }
            }

            // Sla de originele positie op voor verticale beweging
            float originalY = position.Y;
            position.Y += velocity.Y;
            UpdateBounds();

            foreach (var tile in tiles)
            {
                if (bounds.Intersects(tile.Rectangle))
                {
                    if (velocity.Y > 0) // Val naar beneden
                    {
                        position.Y = tile.Rectangle.Top - bounds.Height;
                        isGrounded = true;
                        isJumping = false;
                    }
                    else if (velocity.Y < 0) // Spring omhoog
                    {
                        position.Y = tile.Rectangle.Bottom;
                    }
                    velocity.Y = 0;
                    UpdateBounds();
                }
            }
        }

        protected virtual void UpdateBounds()
        {
            bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                COLLISION_WIDTH,
                COLLISION_HEIGHT
            );
        }

        protected virtual void UpdateAnimation(GameTime gameTime)
        {
            if (isMoving)
            {
                currentAnimation = runAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

            currentAnimation?.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentAnimation?.CurrentFrame != null)
            {
                spriteBatch.Draw(
                    playerTexture,
                    position,
                    currentAnimation.CurrentFrame.SourceRecatangle,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0f
                );
            }

            // Debug collision bounds
            #if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Red * 0.5f });
            spriteBatch.Draw(boundTexture, bounds, Color.Red * 0.3f);
            #endif
        }

        public virtual void SetAnimationSpeed(float runSpeed, float idleSpeed)
        {
            runAnimation?.SetSpeed(runSpeed);
            idleAnimation?.SetSpeed(idleSpeed);
        }

        public virtual void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public Rectangle Bounds => bounds;

        public virtual void Bounce()
        {
            velocity.Y = jumpForce / 2;
            isJumping = true;
            isGrounded = false;
        }

        public virtual void TakeDamage()
        {
            // Implementeer hero damage logica
        }
    }
}
