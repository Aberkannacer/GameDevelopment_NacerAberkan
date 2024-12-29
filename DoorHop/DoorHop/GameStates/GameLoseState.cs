using DoorHop.Buttons;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DoorHop.Players.Heros;

namespace DoorHop.GameStates
{
    internal class GameLoseState : State
    {
        //rectangle texture
        private Texture2D gameOverTexture;
        //buttons
        private StartButton playAgainButton;
        private EndButton endButton; 
        //hero
        private Hero hero;
        public GameLoseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Hero hero) : base(game, content)
        {
            this.hero = hero;
            //button
            playAgainButton = new StartButton(game, graphicsDevice, content, new Vector2(350, 200), "Play Again", hero);
            endButton = new EndButton(game, graphicsDevice, content, new Vector2(350, 250), "Quit");
            buttons.Add(playAgainButton);
            buttons.Add(endButton);
            //background
            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            Load();
        }
        public virtual void Load()
        {
            gameOverTexture = content.Load<Texture2D>("gameOverScreen");
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOverTexture, backgroundRect, Color.White);
            foreach (var item in buttons)
            {
                item.Draw(spriteBatch);
            }

        }
        public override void LoadContent()
        {
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var item in buttons)
            {
                item.Update(gameTime);
            }
        }
    }
}
