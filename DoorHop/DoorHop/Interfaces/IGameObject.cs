using DoorHop.Players;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Interfaces
{
    internal interface IGameObject
    {
        void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero);
        void Draw(SpriteBatch spriteBatch);
    }
}