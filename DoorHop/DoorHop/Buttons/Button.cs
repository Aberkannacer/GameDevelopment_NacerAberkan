using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DoorHop.Buttons
{
    internal abstract class Button
    {
        //button
        protected Rectangle button;
        private int height, width;
        //mouse
        protected MouseState previousMouseState;
        //game & texture
        protected Game1 game;
        protected GraphicsDevice graphicsDevice;
        protected ContentManager content;
        protected Texture2D texture;
        private SpriteFont font;
        private string text;

        public Button(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Vector2 position, string text)
        {
            //game & texture
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            this.texture = new Texture2D(graphicsDevice, 1, 1);
            this.texture.SetData(new[] { Color.White });
            this.font = content.Load<SpriteFont>("MyFont");
            this.text = text;
            //button
            height = 30;
            width = 100;
            button = new Rectangle((int)position.X, (int)position.Y, width, height);
        }
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, button, Color.White);
            Vector2 textSize = font.MeasureString(text);
            Vector2 textPos = new Vector2(
                button.X + (button.Width - textSize.X) / 2,
                button.Y + (button.Height - textSize.Y) / 2);

            spritebatch.DrawString(font, text, textPos, Color.Black);
        }
        protected abstract void OnClick();

        public virtual void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Point mousePosition = new Point(currentMouseState.X, currentMouseState.Y);

            if (button.Contains(mousePosition) && currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                OnClick();
            }
            if (button.Contains(mousePosition))
            {
                texture.SetData(new[] { Color.LightBlue });
            }
            else
            {
                texture.SetData(new[] { Color.White });
            }
            previousMouseState = currentMouseState;
        }
    }
}
