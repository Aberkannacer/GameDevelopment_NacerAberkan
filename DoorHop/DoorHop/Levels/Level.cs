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
        private Texture2D doorTexture;
        private Door door;

        private Rectangle backgroundRect;
        protected Map map;
        protected ContentManager content;
        protected List<Enemy> enemies;
        protected List<Collectable> coins;
        protected Hero hero;
        protected int[,] levelOne;

        protected int collectedCoins;
        protected int totalCoins = 3;

        private HealthHeart healthHeart;

        protected Level(ContentManager content, Hero hero, GraphicsDevice graphicsDevice)
        {


            this.content = content;
            this.hero = hero;
            map = new Map();
            enemies = new List<Enemy>();
            coins = new List<Collectable>();

            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public virtual void Load()
        {
            backGround = content.Load<Texture2D>("background");
            healthHeart = new HealthHeart(content, hero, new Vector2(670, 10));
            doorTexture = content.Load<Texture2D>("DoorTexture"); // Zorg ervoor dat je een deur texture hebt
            door = new Door(doorTexture, new Vector2(400, 200));

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
            if (collectedCoins == totalCoins) // totalCoins kan nu ook coins.Count zijn
            {
                map.ChangeTileValue(1, 0, levelOne);
                map.Generate(levelOne, 30);
                System.Diagnostics.Debug.WriteLine("Victory!");
            }

            // Controleer of de speler bij de deur staat
            if (hero.Bounds.Intersects(door.Bounds) && InputManager.IsActionButtonPressed()) // Zorg ervoor dat je een input manager hebt
            {
                // Ga naar level 2
                game.ChangeState(new Level2State(game, content)); // Zorg ervoor dat je een Level2State hebt
            }


        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, backgroundRect, Color.White);
            door.Draw(spriteBatch);
            foreach (var enemy in enemies)
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
