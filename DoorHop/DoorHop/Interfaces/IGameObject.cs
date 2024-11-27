using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Interfaces
{
    internal interface IGameObject
    {
        void Update(GameTime gameTime, List<CollisionTiles> tiles);

        void Draw(SpriteBatch spriteBatch);
    }
}
