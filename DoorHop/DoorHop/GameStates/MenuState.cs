using DoorHop.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.GameStates
{
    internal class MenuState : State
    {
        private StartButton startButton;
        private EndButton endButton;
        private Texture2D texture;
        private GraphicsDevice graphicsDevice;
        public MenuState(Game1 game, ContentManager content, GraphicsDevice graphicsDevice) : base(game, content)
        {
            startButton = new StartButton(game, graphicsDevice, content, new Vector2(350,200 ), "Start");
            endButton = new EndButton(game, graphicsDevice, content, new Vector2(350, 250), "Quit");
            buttons.Add(startButton);
            buttons.Add(endButton);
            this.graphicsDevice = graphicsDevice;
            texture = content.Load<Texture2D>("BackGroundMainMenu");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);

            foreach (Button item in buttons)
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
            foreach (Button item in buttons)
            {
                item.Update(gameTime);

            }
        }
    }
}
