using DoorHop.Players;
using DoorHop.Players.Enemys;
using DoorHop.Players.Hero;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using DoorHop.Input;

namespace DoorHop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Players.Hero.Hero hero;
        private Map map;
        private List<Enemy> enemies;
        private IInputReader inputReader;

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
            hero = new Players.Hero.Hero(Content, inputReader);

            WalkEnemy walkEnemy = new WalkEnemy(Content,64,64);
            enemies.Add(walkEnemy);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null
            );
            
            map.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            
            if (enemies != null)
            {
                foreach (var enemy in enemies.Where(e => e != null))
                {
                    
                    enemy.Draw(_spriteBatch);
                }
            }
            
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
                            hero.TakeDamage();
                        }
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
