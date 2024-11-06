using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DoorHop
{
    public class Game1 : Game
    {
        Texture2D shardsoulTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle deelRectangle;
        private int schijfOp_X = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            deelRectangle = new Rectangle(schijfOp_X, 0, 64, 64);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            shardsoulTexture = Content.Load<Texture2D>("Shardsoul Slayer Sprite Sheet");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(shardsoulTexture, new Vector2(0, 0),deelRectangle, Color.White);
            _spriteBatch.End();

            schijfOp_X = 64;
            if (schijfOp_X > 448)
            {
                schijfOp_X = 0;
            }

            deelRectangle.X = schijfOp_X;
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
