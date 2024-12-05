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
    internal abstract class Enemy: IGameObject
    {
        protected Texture2D texture;
        protected Animatie animatie;
        protected Rectangle rectangle;
        protected Vector2 position;
        bool IsAlive;

        protected Enemy(Texture2D texture, int row, int col, int width, int height)
        {
            IsAlive = true;
            Width = width;
            Height = height;
            //this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            //this.position = CalculatePositionInMap.CalculatePosition(row, col, width, height);

        }

        private int score;
        public int Score
        {
            get
            {
                return score;
            }
            protected set
            {
                score = value;
            }
        }

        protected int Width 
        { 
            get; 
            set; 
        }
        protected int Height 
        { 
            get; 
            set; 
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



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //tekenen als de enemy nog leeft
            if (IsAlive)
            {

                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }

        public abstract void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles);


        public void Die()
        {
            //Enemy is dood
            IsAlive = false;
        }
    }
}
