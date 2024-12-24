using DoorHop.Animation;
using DoorHop.Interfaces;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    internal abstract class Enemy : IGameObject
    {
        protected Texture2D texture;
        protected Animatie currentAnimation;
        protected Hero hero;
        protected Rectangle rectangle;
        protected Vector2 position;
        protected bool isAlive;
        protected Rectangle bounds;
        protected float moveSpeed;

        protected Enemy(int width, int height)
        {
            position = Vector2.Zero;
            isAlive = true;
            //bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }
        public virtual Rectangle HitBox
        {
            get
            {
                return rectangle;
            }
            protected set
            {
                rectangle = value;
            }
        }


        public abstract void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles);

        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle Bounds => bounds;
        public virtual void TakeDamage() => isAlive = false;

        
    }
}
