using DoorHop.Collectables;
using DoorHop.GameStates;
using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace DoorHop.Levels
{
    internal abstract class Level
    {
        //texture & rectangle
        private Texture2D backGround;
        private Rectangle backgroundRect;
        protected ContentManager content;
        public HealthHeart healthHeart;
        public GraphicsDevice graphicsDevice;
        //map
        protected Map map;
        protected int[,] levelOne;
        //enemy & coin
        protected List<Enemy> enemies;
        protected List<Collectable> coins;
        protected int collectedCoins;
        protected int totalCoins = 3;
        //hero
        protected Hero hero;
        //game
        protected Game1 game;

        protected Level(ContentManager content, Hero hero, GraphicsDevice graphicsDevice, Game1 game)
        {
            //content
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            //hero & game
            this.hero = hero;
            this.game = game;
            //map
            map = new Map();
            //enemy & coin
            enemies = new List<Enemy>();
            coins = new List<Collectable>();
            //background
            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }
        public virtual void Load()
        {
            backGround = content.Load<Texture2D>("background");
            healthHeart = new HealthHeart(content, hero, new Vector2(670, 10));
        }
        protected void CheckEnemyCollisions()
        {
            foreach (var enemy in enemies.ToList())
            {
                if (!enemy.isAlive)
                {
                    hero.AddScore(enemy.Score);//score wordt toegevoegd
                    enemies.Remove(enemy);//enemy wordt verwijderd
                }
            }
        }
        //logica van het spel
        public virtual void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (hero != null && map != null)
            {
                hero.Update(gameTime, map.CollisionTiles, hero, enemies);
                healthHeart = new HealthHeart(content, hero, new Vector2(670, 10));

                #region enemy killen als je op hun springt
                foreach (var enemy in enemies.ToList()) // Gebruik ToList() voor veilige verwijdering
                {
                    // Controleer of de hero op deze vijand springt
                    if (enemy.CollisionCheck(hero) && hero.velocity.Y > 0 && hero.Bounds.Bottom <= enemy.Bounds.Top + 10)
                    {
                        // De hero springt op de vijand
                        enemy.TakeDamage(); // Markeer de vijand als dood
                        hero.KillSound();
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


                }
                #endregion
                #region enemy killen
                foreach (var enemy in enemies)
                {
                    enemy.Update(gameTime, map.CollisionTiles, hero, enemies);

                    if (hero.Bounds.Intersects(enemy.Bounds))
                    {
                        if (hero.Bounds.TouchTopOf(enemy.Bounds))
                        {
                            hero.Bounce();
                            enemy.TakeDamage();
                            hero.KillSound();
                        }
                    }
                    #endregion
                    #region alles wat te maken heeft met schieten logica
                    if (enemy is ShootEnemy shootEnemy)
                    {
                        for (int i = shootEnemy.Bullets.Count - 1; i >= 0; i--)
                        {
                            var bullet = shootEnemy.Bullets[i];
                            if (bullet.CollisionCheck(hero))
                            {
                                hero.GetHit(1);
                                hero.GetHitSound();
                                shootEnemy.RemoveBullet(bullet);
                                
                            }
                        }
                    }
                }
                #endregion
                #region coin
                foreach (var coin in coins.ToList()) // Gebruik ToList() voor veilige verwijdering
                {
                    coin.Update(gameTime, hero); // Update de coin

                    if (coin.CollisionCheck(hero)) // Controleer of de hero de coin heeft verzameld
                    {
                        collectedCoins++; // Verhoog het aantal verzamelde coins
                        coins.Remove(coin); // Verwijder de coin uit de lijst
                        hero.CollectCoin();
                    }
                }
                

            }
            if (collectedCoins == totalCoins) // totalCoins kan nu ook coins.Count zijn
            {
                map.ChangeTileValue(1, 0, levelOne);
                map.Generate(levelOne, 30);
            }
            #endregion
            #region hero is dood
            if (hero.isDead)
            {
                hero.isDead = false;
                hero.DeathSound();
                game.ChangeState(new GameLoseState(game, game.GraphicsDevice, game.Content, hero));
            }
            #endregion
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, backgroundRect, Color.White);

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (var coin in coins)
            {
                coin.Draw(spriteBatch);
            }
            healthHeart.Draw(spriteBatch);
            hero.Draw(spriteBatch);
        }
    }
}
