using DoorHop.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Levels
{
    internal class Door
    {
        private Texture2D texture;
        private Rectangle bounds;

        public Door(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            int doorWidth = texture.Width / 10; // Bijvoorbeeld, maak de deur half zo breed
            int doorHeight = texture.Height/10;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, doorWidth, doorHeight);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        public Rectangle Bounds => bounds;
    }
}
