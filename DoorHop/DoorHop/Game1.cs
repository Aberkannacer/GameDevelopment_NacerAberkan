using DoorHop.Input;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DoorHop
{
    public class Game1 : Game
    {
        Texture2D shardsoulTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map map;

        Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            shardsoulTexture = Content.Load<Texture2D>("Shardsoul Slayer Sprite Sheet");



            Tiles.Content = Content;

            map.Generate(new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
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
            // TODO: use this.Content to load your game content here

            InitializeGameObjects();

        }

        private void InitializeGameObjects()
        {
            player = new Player(shardsoulTexture, new KeyBoardReader());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            map.Draw(_spriteBatch);

            _spriteBatch.End();

            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
