using DoorHop.Buttons;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using DoorHop.Players.Heros;

namespace DoorHop.GameStates
{
    internal class GameLoseState : State
    {
        private Rectangle backgroundRect;
        private Texture2D gameOverTexture;
        private StartButton startButton;
        private EndButton endButton;

        public GameLoseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, content)
        {
            //int xpos = ScreenSettings.ScreenWidth / 2 - 50;
            startButton = new StartButton(game, graphicsDevice, content, new Vector2(350, 200), "Start");
            endButton = new EndButton(game, graphicsDevice, content, new Vector2(350, 250), "Quit");
            buttons.Add(startButton);
            buttons.Add(endButton);
            Load();
            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
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

        public override void PostUpdate(GameTime gameTime)
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
