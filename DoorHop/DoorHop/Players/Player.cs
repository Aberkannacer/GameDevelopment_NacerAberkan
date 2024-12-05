using DoorHop.Animation;
using DoorHop.Input;
using DoorHop.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace DoorHop.Players
{
    public abstract class Player : IGameObject
    {
        protected Texture2D playerTexture;
        protected Animatie animatie;
        protected Vector2 position;
        protected Vector2 velocity;
        protected IInputReader inputReader;
        protected int frameWidth = 64;
        protected int frameHeight = 64;
        protected float moveSpeed = 5f;
        protected float jumpForce = -12f;
        protected float gravity = 0.5f;
        protected bool isMoving;
        protected bool isJumping = false;

        protected Player(ContentManager content, IInputReader inputReader)
        {
            position = new Vector2(100, 300);
            velocity = Vector2.Zero;
            this.inputReader = inputReader;
        }

        //voor de content van de players
        protected abstract void LoadContent(ContentManager contentManager);


        public virtual void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            Vector2 oldPosition = position;
            HandleInput();

            // Pas zwaartekracht toe
            velocity.Y += gravity;

            // Update positie voor X en Y apart
            position.X += velocity.X;
            CheckXCollision(tiles);  // Check horizontale collision eerst

            position.Y += velocity.Y;
            CheckYCollision(tiles);  // Check verticale collision daarna

            // Animatie update
            isMoving = Math.Abs(velocity.X) > 0.1f;
            if (isMoving)
            {
                animatie.Update(gameTime);
            }
            else
            {
                animatie.Reset();
            }
        }

        protected virtual void CheckXCollision(List<TileMap.CollisionTiles> tiles)
        {
            if (tiles == null) return;

            Rectangle playerRect = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);

            foreach (var tile in tiles)
            {
                if (playerRect.Intersects(tile.Rectangle))
                {
                    // Als we tegen een muur aanlopen
                    if (velocity.X > 0) // Beweging naar rechts
                    {
                        position.X = tile.Rectangle.Left - frameWidth;
                        velocity.X = 0;
                    }
                    else if (velocity.X < 0) // Beweging naar links
                    {
                        position.X = tile.Rectangle.Right;
                        velocity.X = 0;
                    }
                }
            }
        }

        protected virtual void CheckYCollision(List<TileMap.CollisionTiles> tiles)
        {
            if (tiles == null) return;

            Rectangle playerRect = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);

            foreach (var tile in tiles)
            {
                if (playerRect.Intersects(tile.Rectangle))
                {
                    // Verticale collision
                    if (velocity.Y > 0) // Val naar beneden
                    {
                        position.Y = tile.Rectangle.Top - frameHeight;
                        velocity.Y = 0;
                        isJumping = false;
                    }
                    else if (velocity.Y < 0) // Spring omhoog
                    {
                        position.Y = tile.Rectangle.Bottom;
                        velocity.Y = 0;
                    }
                }
            }
        }

        protected virtual void HandleInput()
        {
            Vector2 direction = inputReader.ReadInput();
            velocity.X = direction.X * moveSpeed;

            if (inputReader.IsJumpKeyPressed() && !isJumping)
            {
                velocity.Y = jumpForce;
                isJumping = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
                SpriteEffects effect = velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
               
                
                spriteBatch.Draw(playerTexture,position,animatie.CurrentFrame.SourceRecatangle,
                    Color.White,0f,Vector2.Zero,1f,effect,0f);


        }

        public abstract void SetAnimationSpeed(float speed);

        public virtual void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public virtual void SetJumpForce(float force)
        {
            // Zorg ervoor dat jumpForce negatief blijft
            jumpForce = -Math.Abs(force); 
        }
    }
}
