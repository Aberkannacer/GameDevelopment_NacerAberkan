using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Interfaces
{
    internal interface IGameObject
    {
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies);
        void Draw(SpriteBatch spriteBatch);
    }
}