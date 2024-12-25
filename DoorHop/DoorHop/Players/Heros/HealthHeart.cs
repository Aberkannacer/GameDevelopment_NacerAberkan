using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DoorHop.Players;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DoorHop.Animation;

namespace DoorHop.Players.Heros
{
    public class HealthHeart
    {
        private Texture2D heartTexture;
        private Vector2 position;
        private int heartSpacing = 20;
        private Hero hero;
        private Rectangle[] heartFrames; // Array voor elke heart positie

        public HealthHeart(ContentManager content, Hero hero, Vector2 startPosition)
        {
            this.hero = hero;
            this.position = startPosition;
            LoadContent(content);
            
            // Maak rectangles voor elke heart in de sprite
            heartFrames = new Rectangle[3];
            int heartWidth = heartTexture.Width / 3;
            int heartHeight = heartTexture.Height;
            
            for (int i = 0; i < 3; i++)
            {
                heartFrames[i] = new Rectangle(i * heartWidth, 0, heartWidth, heartHeight);
            }
        }

        public void LoadContent(ContentManager content)
        {
            heartTexture = content.Load<Texture2D>("Heart");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hero.Health; i++)
            {
                spriteBatch.Draw(
                    heartTexture,
                    new Vector2(position.X + (heartFrames[0].Width + heartSpacing) * i, position.Y),
                    heartFrames[0], // Gebruik de eerste heart uit de sprite
                    Color.White,
                    0f,
                    Vector2.Zero,
                    2f, // Scale voor betere zichtbaarheid
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
