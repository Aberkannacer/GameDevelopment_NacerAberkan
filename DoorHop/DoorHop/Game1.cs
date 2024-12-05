using DoorHop.Players;
using DoorHop.Players.Enemys;
using DoorHop.Players.Hero;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DoorHop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Hero hero;
        private Map map;
        private List<Enemy> enemies;

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
            
            //map tekeken 
            map = new Map();
            map.Generate(new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,4,4,4,0,0,0,0,4,4,4,4,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0 },
                { 4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4 },
                { 0,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0 },
                { 0,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0 },
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 },
            }, 48);

            //hero tekenen 
            hero = new Hero(Content);

            WalkEnemy walkEnemy = new WalkEnemy(Content, null, 0, 0, 64, 64);
            walkEnemy.SetPosisition(new Vector2(400, 200));
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

            _spriteBatch.Begin();
            
            map?.Draw(_spriteBatch);
            
            hero?.Draw(_spriteBatch);
            
            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
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
                }
            }

            base.Update(gameTime);
        }
    }
}
