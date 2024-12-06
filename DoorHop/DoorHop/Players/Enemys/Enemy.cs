using DoorHop.Animation;
using DoorHop.Interfaces;
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
        protected Vector2 position;
        protected bool isAlive;
        protected Rectangle bounds;
        protected float moveSpeed;

        protected Enemy(int width, int height)
        {
            isAlive = true;
            position = new Vector2(400, 386);
            moveSpeed = 2f;
            bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public virtual void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (!isAlive) return;
            position.X += moveSpeed;
            if (position.X > 400 || position.X < 0) moveSpeed = -moveSpeed;
            bounds = new Rectangle((int)position.X, (int)position.Y, bounds.Width, bounds.Height);
            currentAnimation?.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!isAlive || currentAnimation?.CurrentFrame == null) return;
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.SourceRecatangle,
                Color.White, 0f, Vector2.Zero, 2f,
                moveSpeed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
        }

        public Rectangle Bounds => bounds;
        public virtual void TakeDamage() => isAlive = false;
    }
}
