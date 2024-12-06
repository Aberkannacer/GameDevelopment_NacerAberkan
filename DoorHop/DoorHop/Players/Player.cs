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
        protected Animatie currentAnimatie;
        protected Vector2 position;
        protected Vector2 velocity;
        protected IInputReader inputReader;
        protected float moveSpeed = 5f;
        protected float jumpForce = -12f;
        protected float gravity = 0.5f;
        protected bool isMoving;
        protected bool isJumping = false;

        
        /*protected Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffsetX,
                    (int)position.Y + collisionOffsetY,
                    collisionWidth,
                    collisionHeight
                );
            }
        }*/

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
            Vector2 input = inputReader.ReadInput();
            velocity.X = input.X * moveSpeed;
            velocity.Y += gravity;

            if (isJumping && input.Y < 0)
            {
                velocity.Y = jumpForce;
                isJumping = false;
            }

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Collision checks
            CheckXCollision(tiles);
            CheckYCollision(tiles);
        }

        protected virtual void CheckXCollision(List<TileMap.CollisionTiles> tiles)
        {
            foreach (var tile in tiles)
            {
                if (new Rectangle((int)position.X, (int)position.Y, 64, 64).Intersects(tile.Rectangle))
                {
                    velocity.X = 0;
                }
            }
        }

        protected virtual void CheckYCollision(List<TileMap.CollisionTiles> tiles)
        {
            foreach (var tile in tiles)
            {
                if (new Rectangle((int)position.X, (int)position.Y, 64, 64).Intersects(tile.Rectangle))
                {
                    velocity.Y = 0;
                    isJumping = false;
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
               
                
                spriteBatch.Draw(playerTexture,position, currentAnimatie.CurrentFrame.SourceRecatangle,
                    Color.White,0f,Vector2.Zero,1f,effect,0f);


        }

        public abstract void SetAnimationSpeed(float runSpeed, float idleSpeed);

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
