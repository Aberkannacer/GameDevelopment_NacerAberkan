using DoorHop.Collectables;
using DoorHop.Players.Enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Interfaces
{
    interface IGameObjectFactory
    {
        Enemy CreateEnemy(string type, ContentManager content, Vector2 position);
        Collectable CreateCoin(Vector2 position, ContentManager content);
    }
}
