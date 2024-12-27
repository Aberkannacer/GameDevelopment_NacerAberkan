using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

        public Rectangle Bounds => bounds;
    }
}
