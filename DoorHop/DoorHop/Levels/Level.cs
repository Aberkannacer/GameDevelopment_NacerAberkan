using DoorHop.Collectables;
using DoorHop.GameStates;
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

namespace DoorHop.Levels
{
    internal abstract class Level
    {
        private Texture2D backGround;
        

        private Rectangle backgroundRect;
        protected Map map;
        protected ContentManager content;
        protected List<Enemy> Enemies;
        protected List<Collectable> coins;
        protected Hero hero;
        protected int[,] levelOne;

        protected int collectedCoins;
        protected int totalCoins = 3;

        private HealthHeart healthHeart;
        public GraphicsDevice graphicsDevice;


        protected Game1 game;


        protected Level(ContentManager content, Hero hero, GraphicsDevice graphicsDevice, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;

            this.content = content;
            this.hero = hero;
            this.game = game;
            map = new Map();
            Enemies = new List<Enemy>();
            coins = new List<Collectable>();

            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public virtual void Load()
        {
            backGround = content.Load<Texture2D>("background");
            healthHeart = new HealthHeart(content, hero, new Vector2(670, 10));
            

        }

        public virtual void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (hero != null && map != null)
            {
                hero.Update(gameTime, map.CollisionTiles, hero, Enemies);

                #region enemy killen als je op hun springt
                foreach (var enemy in Enemies.ToList()) // Gebruik ToList() voor veilige verwijdering
                {
                    // Controleer of de hero op deze vijand springt
                    if (enemy.CollisionCheck(hero) && hero.velocity.Y > 0 && hero.Bounds.Bottom <= enemy.Bounds.Top + 10)
                    {
                        // De hero springt op de vijand
                        enemy.TakeDamage(); // Markeer de vijand als dood

                        // Geef de hero een bounce effect door zijn velocity aan te passen
                        hero.velocity.Y = -10f; // Pas dit aan op basis van je bounce kracht
                    }
                    else if (enemy.CollisionCheck(hero))
                    {
                        // Controleer of de hero geraakt wordt door de vijand
                        if (!hero.isDead) // Alleen damage doen als de hero nog leeft
                        {
                            hero.GetHit(1);
                        }
                    }

                    // Verwijder de vijand als deze dood is
                    if (!enemy.isAlive)
                    {
                        Enemies.Remove(enemy);
                    }
                }
                #endregion
                foreach (var enemy in Enemies)
                {
                    enemy.Update(gameTime, map.CollisionTiles, hero, Enemies);

                    if (hero.Bounds.Intersects(enemy.Bounds))
                    {
                        if (hero.Bounds.TouchTopOf(enemy.Bounds))
                        {
                            hero.Bounce();
                            enemy.TakeDamage();
                        }
                    }

                    if (enemy is ShootEnemy shootEnemy)
                    {
                        for (int i = shootEnemy.Bullets.Count - 1; i >= 0; i--)
                        {
                            var bullet = shootEnemy.Bullets[i];
                            if (bullet.CollisionCheck(hero))
                            {
                                hero.GetHit(1);
                                shootEnemy.RemoveBullet(bullet);
                            }
                        }
                    }
                }
                foreach (var coin in coins.ToList()) // Gebruik ToList() voor veilige verwijdering
                {
                    coin.Update(gameTime, hero); // Update de coin

                    if (coin.CollisionCheck(hero)) // Controleer of de hero de coin heeft verzameld
                    {
                        collectedCoins++; // Verhoog het aantal verzamelde coins
                        coins.Remove(coin); // Verwijder de coin uit de lijst
                    }
                }

            }
            if (collectedCoins == totalCoins) // totalCoins kan nu ook coins.Count zijn
            {

                map.ChangeTileValue(1, 0, levelOne);
                map.Generate(levelOne, 30);
                System.Diagnostics.Debug.WriteLine("Victory!");
            }

            if (hero.isDead)
            {
                System.Diagnostics.Debug.WriteLine("Game Over!");
                game.ChangeState(new GameLoseState(game,graphicsDevice ,game.Content)); // Ga naar GameLostState
            }




        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, backgroundRect, Color.White);

            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (var coin in coins)
            {
                coin.Draw(spriteBatch);
            }
            hero.Draw(spriteBatch);
            healthHeart.Draw(spriteBatch);
        }
    }
}
