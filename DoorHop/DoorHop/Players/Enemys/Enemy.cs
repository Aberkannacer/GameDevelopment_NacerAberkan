using DoorHop.Animation;
using DoorHop.Interfaces;
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
        protected Animatie currentAnimatie;
        protected Rectangle rectangle;
        protected Vector2 position;
        protected Vector2 velocity;
        protected bool isAlive;
        protected int width;
        protected int height;
        protected float moveSpeed;

        protected Enemy(Texture2D texture, int width, int height)
        {
            isAlive = true;
            this.width = width;
            this.height = height;
            this.texture = texture;
            position = new Vector2(400, 200);
            velocity = Vector2.Zero;
            moveSpeed = 2f;

            UpdateRectangle();
        }

        protected int Width { get; set; }
        protected int Height { get; set; }

        public Rectangle HitBox
        {
            get { return rectangle; }
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles);

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        protected void UpdateRectangle()
        {
            rectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                width,
                height
            );
        }

        protected virtual void CheckCollision(List<TileMap.CollisionTiles> tiles)
        {
            if (tiles == null) return;

            foreach (var tile in tiles)
            {
                if (rectangle.Intersects(tile.Rectangle))
                {
                    HandleCollision(tile);
                }
            }
        }

        protected virtual void HandleCollision(TileMap.CollisionTiles tile)
        {
            velocity = Vector2.Zero;
        }
        public bool IsAlive()
        {
            return isAlive;
        }

        public void SetAlive(bool alive)
        {
            isAlive = alive;
        }

    }
}
