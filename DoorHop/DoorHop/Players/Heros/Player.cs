using DoorHop.Animation;
using DoorHop.Input;
using DoorHop.Interfaces;
using DoorHop.Players.Enemys;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DoorHop.Players.Heros
{
    public abstract class Player : IGameObject
    {
        //texture
        protected Texture2D playerTexture;
        //animations
        protected Animatie currentAnimation;
        protected Animatie runAnimation;
        protected Animatie idleAnimation;
        protected Animatie attackAnimation;
        protected Animatie jumpAnimation;
        protected Animatie dieAnimation;
        //game
        protected Game game;
        //vectors
        public Vector2 position;
        public Vector2 velocity;
        //input
        protected IInputReader inputReader;
        //attributen
        protected float moveSpeed;
        protected float jumpForce;
        protected bool isMoving;
        protected bool isJumping;
        protected bool isGrounded;
        protected bool isFacingRight;
        protected bool isAttacking;
        public bool dead;
        //bounds & collision
        protected Rectangle bounds;
        protected int collisionWidth;
        protected int collisionHeight;
        //ontkwetsbaar
        protected bool isInvulnerable;
        protected float invulnerabilityTimer;
        protected float invulnerabilityDuration;
        protected Player(ContentManager content, IInputReader inputReader, Game game)
        {
            this.inputReader = inputReader;
            //velo
            velocity = Vector2.Zero;
            //attributen
            isJumping = false;
            isMoving = false;
            isGrounded = false;
            isFacingRight = true;
            //speed
            moveSpeed = 5f;
            //collision
            collisionWidth = 48;
            collisionHeight = 48;
            //ontkwetsbaar
            isInvulnerable = false;
            invulnerabilityTimer = 0f;
            invulnerabilityDuration = 1.5f;
            //game
            this.game = game;
        }
        public virtual void LoadContent(ContentManager content)
        {
        }
        public virtual void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies)
        {
            if (isInvulnerable)
            {
                invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (invulnerabilityTimer <= 0)
                {
                    isInvulnerable = false;
                }
            }
            Vector2 input = inputReader.ReadInput();
            velocity.X = input.X * moveSpeed;

            if (input.X < 0) isFacingRight = false;
            else if (input.X > 0) isFacingRight = true;

            isMoving = Math.Abs(velocity.X) > 0;
            //attacken van enemies
            if (inputReader.IsAttackButtonPressed())
            {
                Attack(enemies);
            }
            // Handle collisions
            HandleCollisions(tiles);
            UpdateAnimation(gameTime);
        }
        protected virtual void HandleCollisions(List<TileMap.CollisionTiles> tiles)
        {
            isGrounded = false;

            float originalX = position.X;
            position.X += velocity.X;
            UpdateBounds();
            foreach (var tile in tiles)
            {
                if (bounds.Intersects(tile.Rectangle))
                {
                    position.X = originalX;
                    velocity.X = 0;
                    UpdateBounds();
                    break;
                }
            }
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
                collisionWidth,
                collisionHeight
            );
        }
        protected virtual void UpdateAnimation(GameTime gameTime)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentAnimation?.CurrentFrame != null)
            {
                spriteBatch.Draw(playerTexture, position, currentAnimation.CurrentFrame.sourceRecatangle,
                    Color.White, 0f, Vector2.Zero, 1f, isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            // Debug collision bounds
            /*
            #if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Red * 0.5f });
            spriteBatch.Draw(boundTexture, bounds, Color.Red * 0.3f);
            #endif*/
        }
        public Rectangle Bounds => bounds;
        public virtual void Bounce()
        {
            velocity.Y = jumpForce / 2;
            isJumping = true;
            isGrounded = false;
        }
        public void Attack(List<Enemy> enemies)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                
                attackAnimation.Reset();
            }
            foreach (var enemy in enemies)
            {
                if (bounds.Intersects(enemy.Bounds))
                {
                    enemy.TakeDamage();
                }
            }
        }
    }
}