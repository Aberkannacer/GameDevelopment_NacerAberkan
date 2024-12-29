using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DoorHop.GameStates;

namespace DoorHop
{
    public class Game1 : Game
    {
        //grafics
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        //textures
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;
        //states
        private State currentState;
        private State nextState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //schermbreedte en hoogt
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
            ChangeState(new MenuState(this, Content, graphics.GraphicsDevice));
        }

        protected override void LoadContent()
        {
            //sprite
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //state
            currentState = new MenuState(this, Content, GraphicsDevice);
            currentState.LoadContent();
            //texture
            backgroundTexture = Content.Load<Texture2D>("background");
        }
        protected override void Update(GameTime gameTime)
        {
            {
                if (nextState != null)
                {
                    currentState = nextState;
                    currentState.LoadContent();
                    nextState = null;
                }
                currentState.Update(gameTime);
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            currentState.Draw(gameTime, _spriteBatch);
            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ChangeState(State state)
        {
            nextState = state;
        }
    }
}