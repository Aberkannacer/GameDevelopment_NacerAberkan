using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    internal class ShootEnemy : Enemy
    {
        public ShootEnemy(int width, int height) : base(width, height)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            throw new NotImplementedException();
        }
    }
}
