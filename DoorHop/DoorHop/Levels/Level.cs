using DoorHop.Collectables;
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
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Levels
{
    internal abstract class Level
    {
        protected Map map;
        protected ContentManager content;
        protected List<Enemy> enemies;
        protected List<Collectable> coins;
        protected Hero hero;

        private int collectedCoins; // Aantal verzamelde coins
        private int totalCoins = 3;

        private SpriteFont font;
        Game1 game;
        private HealthHeart healthHeart;
        protected Level(ContentManager content, Hero hero)
        {
            this.content = content;
            this.hero = hero;
            map = new Map();
            enemies = new List<Enemy>();
            coins = new List<Collectable>();
        }

        public virtual void Load()
        {
            // Laad level-specifieke inhoud
        }

        public virtual void Update(GameTime gameTime)
        {



            if (hero != null && map != null)
            {
                hero.Update(gameTime, map.CollisionTiles, hero);

                foreach (var enemy in enemies)
                {
                    enemy.Update(gameTime, map.CollisionTiles, hero);

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




        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (var coin in coins)
            {
                coin.Draw(spriteBatch);
            }
            hero.Draw(spriteBatch);
        }
    }
}
