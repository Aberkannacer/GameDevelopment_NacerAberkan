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

namespace DoorHop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private WalkEnemy walkEnemy;
        private Map map;
        private List<Enemy> enemies;
        private IInputReader inputReader;
        private HealthHeart healthHeart;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;
        Game game;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            enemies = new List<Enemy>();
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
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4  },
                { 4,0,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,4,0,0,0,0  },
                { 4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0 },
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0 },
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
            }, 30);

            inputReader = new KeyBoardReader();
            hero = new Hero(Content, inputReader, this);

            walkEnemy = new WalkEnemy(Content,64,64);
            enemies.Add(walkEnemy);



            healthHeart = new HealthHeart(Content, hero, new Vector2(670, 10));

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;

            backgroundTexture = Content.Load<Texture2D>("background");
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


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
                hero.Update(gameTime, map.CollisionTiles);
                
                foreach (var enemy in enemies)
                {
                    enemy.Update(gameTime, map.CollisionTiles);
                    
                    if (hero.Bounds.Intersects(enemy.Bounds))
                    {
                        if (hero.Bounds.TouchTopOf(enemy.Bounds))
                        {
                            hero.Bounce();
                            enemy.TakeDamage();
                        }
                        else
                        {
                            
                        }
                    }
                }
            }


            //voor check van dat enemy die tegen elkaar botsen

                if (walkEnemy.CollisionCheck(hero))
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
    }
}
