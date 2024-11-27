using DoorHop.Animation;
using DoorHop.Input;
using DoorHop.Interfaces;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop
{
    public class Player : IGameObject
    {
        private Texture2D playerTexture;
        private Animatie animatie;
        private Vector2 position;
        private Vector2 velocity;
        private IInputReader inputReader;
        private int frameWidth = 64;
        private int frameHeight = 64;
        private float moveSpeed = 5f;
        private float jumpForce = -12f;
        private float gravity = 0.5f;
        private bool isMoving;
        private float movementThreshold = 0.1f;
        private float animationSpeed = 0.1f;
        private bool isJumping = false;

        public Player(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player");
            position = new Vector2(100, 100);
            velocity = Vector2.Zero;
            inputReader = new KeyBoardReader();
            
            // Animatie setup
            animatie = new Animatie();
            animatie.AddFrame();
            
        }

        public void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
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

        private void CheckXCollision(List<TileMap.CollisionTiles> tiles)
        {
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

        private void CheckYCollision(List<TileMap.CollisionTiles> tiles)
        {
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

        private void HandleInput()
        {
            Vector2 direction = inputReader.ReadInput();
            velocity.X = direction.X * moveSpeed;

            if (inputReader.IsJumpKeyPressed() && !isJumping)
            {
                velocity.Y = jumpForce;
                isJumping = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = velocity.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            
            spriteBatch.Draw(
                playerTexture, 
                position, 
                animatie.CurrentFrame.SourceRecatangle, 
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                effect,
                0f
            );
        }

        public void SetAnimationSpeed(float speed)
        {
            animationSpeed = speed;
            // Als je Animatie klasse een methode heeft om de snelheid in te stellen, roep die hier aan
            animatie.SetSpeed(animationSpeed);
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public void SetJumpForce(float force)
        {
            jumpForce = -Math.Abs(force); // Zorg ervoor dat jumpForce negatief blijft
        }
    }
}
