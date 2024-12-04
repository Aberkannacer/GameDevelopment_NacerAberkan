using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    internal class WalkEnemy : Enemy
    {
        private float distanceToWalk;
        private float speed;
        



        public WalkEnemy(ContentManager content,Texture2D texture, int row, int col, int width, int height) : base(texture, row, col, width, height)
        {
            texture = content.Load<Texture2D>("Player");
            
        }

        public override Rectangle HitBox 
        {
            get { return base.HitBox; }
            protected set { base.HitBox = value; } 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            throw new NotImplementedException();
        }
    }
}
