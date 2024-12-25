using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoorHop.Levels
{
    internal abstract class Level
    {
        private Texture2D backgroundTexture;


        public virtual void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("background");
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
