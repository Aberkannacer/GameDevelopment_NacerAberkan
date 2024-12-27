using DoorHop.Players;
using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using DoorHop.Input;
using System.Diagnostics;
using DoorHop.Collectables;

namespace DoorHop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private WalkEnemy walkEnemy;
        private ShootEnemy shootEnemy;
        private GhostEnemy ghostEnemy;
        private Map map;
        private List<Enemy> enemies;
        private IInputReader inputReader;
        private HealthHeart healthHeart;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;
        //voor tekst
        private SpriteFont font;

        //voor coin
        private List<Collectable> coins;
        private Texture2D texture;
        private int collectedCoins; // Aantal verzamelde coins
        private int totalCoins = 3;

        Game game;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800; // Stel de breedte in
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            enemies = new List<Enemy>();
            coins = new List<Collectable>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (enemies == null)
            {
                enemies = new List<Enemy>();
            }

            map = new Map();
            map.Generate(new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 0,0,0,4,4,4,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0  },
                { 4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,4  },
                { 4,0,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0 },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0 },
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
            }, 30);

            inputReader = new KeyBoardReader();
            hero = new Hero(Content, inputReader, this);

            walkEnemy = new WalkEnemy(Content, 64, 64);
            shootEnemy = new ShootEnemy(Content, 64, 64);
            ghostEnemy = new GhostEnemy(Content, 64, 64);


            enemies.Add(walkEnemy);
            enemies.Add(shootEnemy);
            enemies.Add(ghostEnemy);



            healthHeart = new HealthHeart(Content, hero, new Vector2(670, 10));


            // Voeg coins toe aan de lijst
            coins.Add(new Collectable(Content, texture, new Vector2(680, 30)));
            coins.Add(new Collectable(Content, texture, new Vector2(440, 90)));
            coins.Add(new Collectable(Content, texture, new Vector2(100, 270)));


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;

            backgroundTexture = Content.Load<Texture2D>("background");
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            font = Content.Load<SpriteFont>("MyFont");


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();

            // Teken eerst de achtergrond
            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            // Dan de rest van je game elementen
            map.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            healthHeart.Draw(_spriteBatch);

            foreach (var coin in coins)
            {
                coin.Draw(_spriteBatch);
            }


            _spriteBatch.DrawString(font, $"To open the door: {collectedCoins}/{totalCoins}", new Vector2(10, 10), Color.White); // Gebruik coins.Count

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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


            //voor check van dat enemy die tegen elkaar botsen

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

            

            // Victory check
            if (collectedCoins == totalCoins) // totalCoins kan nu ook coins.Count zijn
            {
                System.Diagnostics.Debug.WriteLine("Victory!");
            }

            base.Update(gameTime);
        }
    }
}