using DoorHop.Collectables;
using DoorHop.Players.Enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DoorHop.Interfaces
{
    interface IGameObjectFactory
    {
        Enemy CreateEnemy(string type, ContentManager content, Vector2 position);
        Collectable CreateCoin(Vector2 position, ContentManager content);
    }
}
