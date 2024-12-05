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
        protected Animatie animatie;
        protected Rectangle rectangle;
        protected Vector2 position;
        protected bool IsAlive;

        protected Enemy(Texture2D texture, int row, int col, int width, int height)
        {
            IsAlive = true;
            Width = width;
            Height = height;
            position = new Vector2(400, 200); // Default positie
        }

        protected int Width { get; set; }
        protected int Height { get; set; }

        public virtual Rectangle HitBox
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Verwijder de base Draw implementatie
            // Laat het over aan de child classes
        }

        public abstract void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles);

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }
    }
}
