using DoorHop.Collectables;
using DoorHop.Input;
using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DoorHop.Levels
{
    internal class Level1 : Level
    {

        private WalkEnemy walkEnemy;
        private ShootEnemy shootEnemy;
        private GhostEnemy ghostEnemy;


        private SpriteFont font;

        private HealthHeart healthHeart;

        

        public Level1(ContentManager content, Hero hero, GraphicsDevice graphicsDevice) : base(content, hero, graphicsDevice)
        {
            this.hero = hero;
            walkEnemy = new WalkEnemy(content, 64, 64);
            shootEnemy = new ShootEnemy(content, 64, 64);
            ghostEnemy = new GhostEnemy(content, 64, 64);


            enemies.Add(walkEnemy);
            enemies.Add(shootEnemy);
            enemies.Add(ghostEnemy);

            coins.Add(new Collectable(content, new Vector2(680, 30)));
            coins.Add(new Collectable(content, new Vector2(440, 90)));
            coins.Add(new Collectable(content, new Vector2(100, 270)));

            healthHeart = new HealthHeart(content, hero, new Vector2(670, 10));

            //backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }


        public override void Load()
        {
            base.Load();

            Tiles.Content = content;

            font = content.Load<SpriteFont>("MyFont");

            map = new Map();
            levelOne = new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,4,4,4,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0},
                {4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,4},
                {4,0,4,4,4,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            };
            map.Generate(levelOne, 30);

            
        }

        public override void Update(GameTime gameTime)
        {
            
            if (walkEnemy.CollisionCheck(hero) || shootEnemy.CollisionCheck(hero) || ghostEnemy.CollisionCheck(hero))
            {
                if (!hero.isDead) // Alleen damage doen als de hero nog leeft
                {
                    hero.GetHit(1);
                    if (hero.isDead)
                    {

                        System.Diagnostics.Debug.WriteLine("Game Over!");
                    }
                }
            }



            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, $"To open the door: {collectedCoins}/{totalCoins}", new Vector2(10, 10), Color.White);
            map.Draw(spriteBatch);
        }

        
    }
}
