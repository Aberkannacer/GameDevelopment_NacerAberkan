using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DoorHop.Players.Heros
{
    public class HealthHeart
    {
        //textures
        private Texture2D heartTexture;
        //hart
        private Vector2 position;
        private int heartSpacing;
        private Rectangle[] heartFrames;
        //hero
        private Hero hero;
        
        public HealthHeart(ContentManager content, Hero hero, Vector2 startPosition)
        {
            //hero
            this.hero = hero;
            //positiion
            this.position = startPosition;
            //hart
            heartSpacing = 20;
            //hier maakt die een rechthoek van elke hart
            heartFrames = new Rectangle[3];
            LoadContent(content);
            int heartWidth = heartTexture.Width / 3;
            int heartHeight = heartTexture.Height;
            //frames
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
                spriteBatch.Draw(heartTexture,new Vector2(position.X + (heartFrames[0].Width + heartSpacing) * i, position.Y),heartFrames[0], 
                    Color.White, 0f, Vector2.Zero,2f,SpriteEffects.None,0f);
            }
        }
    }
}
