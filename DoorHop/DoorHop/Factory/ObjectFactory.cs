using DoorHop.Collectables;
using DoorHop.Interfaces;
using DoorHop.Players.Enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Factory
{
    internal class ObjectFactory : IGameObjectFactory
    {
        public Enemy CreateEnemy(string type, ContentManager content, Vector2 position)
        {
            return type switch
            {
                "walkEnemy" => new WalkEnemy(content, 64, 64, position),
                "shootEnemy" => new ShootEnemy(content, 64, 64, position),
                "ghostEnemy" => new GhostEnemy(content, 64, 64, position),
                _ => throw new ArgumentException("Invalid enemy type")
            };
        }
        public Collectable CreateCoin(Vector2 position, ContentManager content)
        {
            return new Collectable(content, position);
        }
    }
}
