using DoorHop.Animation;
using DoorHop.Interfaces;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace DoorHop.Players.Enemys
{
    public abstract class Enemy : IGameObject
    {
        protected Texture2D texture;
        protected Animatie currentAnimation;
        protected Rectangle rectangle;
        protected Vector2 position;
        public bool isAlive;
        protected Rectangle bounds;
        protected float moveSpeed;
        //protected int score; // Score voor deze vijand

        protected int score; // Score voor deze vijand

        public int Score
        {
            get { return score; }
            protected set { score = value; }
        }


        protected Enemy(int width, int height)
        {
            position = Vector2.Zero;
            isAlive = true;
            //bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
            //MAG WEG --> test eerst
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            //Score = score;
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

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies);

        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle Bounds => bounds;

        //public int ScoreValue { get => scoreValue; set => scoreValue = value; }

        public virtual void TakeDamage() => isAlive = false;

        public virtual bool CollisionCheck(Hero hero)
        {
            if (hero == null) return false;
            return bounds.Intersects(hero.Bounds);
        }


    }
}