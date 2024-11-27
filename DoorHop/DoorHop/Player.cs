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
    public class Player:IGameObject
    {
        private Texture2D playerTexture;
        private Animatie animatie;
        private Vector2 position;
        //private Vector2 speed;
        private Vector2 velocity;
        private IInputReader inputReader;
        private Rectangle rectangle;
        private bool hasJumped = false;
        private float gravity = 0.5f;
        private int frameWidth = 64;
        private int frameHeight = 64;
        private int currentFrame = 0;
        private int totalFrames = 8;
        private float frameTime = 0.1f;
        private float currentFrameTime = 0;
        private float moveSpeed = 5f;
        private float jumpForce = -12f;
        private bool isJumping = false;

        public Vector2 Position 
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity 
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Player(ContentManager content)
        {      
            playerTexture = content.Load<Texture2D>("Player"); // Controleer of dit bestand bestaat
            inputReader = new KeyBoardReader();
            position = new Vector2(100, 100); // Zet de startpositie hoger
            
                        
            // Initialiseer de animatie
            animatie = new Animatie();
            
            // Voeg eerste frame toe
            animatie.AddFrame(new AnimationFrame(
                new Rectangle(0, 0, frameWidth, frameHeight)
            ));

            rectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }

        public void Load(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player");
        }

        public void Update(GameTime gameTime, List<CollisionTiles> tiles)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);

            Input(gameTime);
            animatie.Update(gameTime);
            
            if (velocity.Y < 10)
                velocity.Y += gravity;

            CheckCollision(tiles);

            // Update de animatie
            currentFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentFrameTime >= frameTime)
            {
                currentFrameTime = 0;
                currentFrame = (currentFrame + 1) % totalFrames;
            }

            // Als de speler niet beweegt, gebruik het eerste frame
            if (velocity.X == 0)
            {
                currentFrame = 0;
            }

            // Grond check (pas aan naar je werkelijke grond Y-positie)
            if (position.Y >= 400) // Voorbeeld grondniveau
            {
                position.Y = 400;
                velocity.Y = 0;
                isJumping = false;
            }
        }

        public void Input(GameTime gameTime)
        {
            Vector2 direction = inputReader.ReadInput();
            velocity.X = direction.X * moveSpeed;

            // Spring logica
            if (inputReader.IsJumpKeyPressed() && !isJumping)
            {
                velocity.Y = jumpForce;
                isJumping = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 2de rij = 64px
            Rectangle sourceRectangle = new Rectangle(
                currentFrame * frameWidth,  // X: 0, 64, 128, 192, etc.
                64,                         // Y: altijd 64 voor deze animatie rij
                frameWidth,                 // 64 pixels breed
                frameHeight                 // 64 pixels hoog
            );

            spriteBatch.Draw(playerTexture,position,sourceRectangle,Color.White
            );
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;

            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + rectangle.Width + 2;

            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (Position.X < 0)
            {
                position.X = 0;
            }
            if (Position.X>xOffset - rectangle.Width)
            {
                position.X = xOffset - rectangle.Width;
            }
            if (Position.Y < 0)
            {
                velocity.Y = 1f;
            }
            if (Position.Y > yOffset - rectangle.Height)
            {
                position.Y = yOffset - rectangle.Height;
            }

        }

        public void CheckCollision(List<CollisionTiles> tiles)
        {
            foreach (var tile in tiles)
            {
                if (rectangle.TouchTopOf(tile.Rectangle))
                {
                    position.Y = tile.Rectangle.Y - rectangle.Height;
                    velocity.Y = 0f;
                    hasJumped = false;
                }
                if (rectangle.TouchLeftOf(tile.Rectangle))
                {
                    position.X = tile.Rectangle.X - rectangle.Width - 2;
                }
                if (rectangle.TouchRightOf(tile.Rectangle))
                {
                    position.X = tile.Rectangle.X + tile.Rectangle.Width + 2;
                }
                if (rectangle.TouchBottomOf(tile.Rectangle))
                {
                    velocity.Y = 1f;
                }
            }
        }

        public void SetAnimationSpeed(float speed)
        {
            // Lagere waarde = snellere animatie
            // Hogere waarde = tragere animatie
            frameTime = speed;
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public void SetJumpForce(float force)
        {
            jumpForce = -Math.Abs(force); // Zorg ervoor dat het altijd negatief is
        }

    }
}
