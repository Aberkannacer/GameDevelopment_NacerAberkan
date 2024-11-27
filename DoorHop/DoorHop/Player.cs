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
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(64, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(128, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(192, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(256, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(320, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(384, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(448, 64, 64, 64)));
        }

        public void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            Vector2 oldPosition = position;
            HandleInput();
            
            // Pas zwaartekracht toe
            velocity.Y += gravity;
            
            // Update positie
            position += velocity;

            // Check beweging voor animatie
            isMoving = Math.Abs(velocity.X) > 0.1f;

            // Update animatie
            if (isMoving)
            {
                animatie.Update(gameTime);
            }
            else
            {
                animatie.Reset();
            }

            // Collision detectie
            foreach (var tile in tiles)
            {
                if (tile.Rectangle.Intersects(new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight)))
                {
                    if (velocity.Y > 0) // Als we naar beneden vallen
                    {
                        position.Y = tile.Rectangle.Top - frameHeight;
                        velocity.Y = 0;
                        isJumping = false; // Reset jumping state als we landen
                    }
                }
            }
        }

        private void HandleInput()
        {
            // Horizontale beweging
            Vector2 direction = inputReader.ReadInput();
            velocity.X = direction.X * moveSpeed;

            // Spring logica - alleen springen als we niet al aan het springen zijn
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
