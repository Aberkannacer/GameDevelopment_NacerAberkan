using DoorHop.Buttons;
using DoorHop.Input;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.GameStates
{
    internal class MenuState : State
    {
        private SpriteFont menuFont;
        private StartButton startButton;
        private EndButton endButton;
        private Texture2D texture;

        private Hero hero;
        private Song backgroundMusic;
        public MenuState(Game1 game, ContentManager content, GraphicsDevice graphicsDevice) : base(game, content)
        {
            hero = new Hero(content, new KeyBoardReader(), game, new Vector2(100, 100));
            startButton = new StartButton(game, graphicsDevice, content, new Vector2(350,200 ), "Start", hero);
            endButton = new EndButton(game, graphicsDevice, content, new Vector2(350, 250), "Quit");
            buttons.Add(startButton);
            buttons.Add(endButton);
            this.graphicsDevice = graphicsDevice;
            texture = content.Load<Texture2D>("BackGroundMainMenu");
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
            spriteBatch.DrawString(menuFont, "Welkom bij het spel DoorHop!", new Vector2(200, 50), Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);

            foreach (Button item in buttons)
            {
                item.Draw(spriteBatch);
            }

        }

        public override void LoadContent()
        {
            menuFont = content.Load<SpriteFont>("MyFont");
            backgroundMusic = content.Load<Song>("BackgroundMusic");
            BackgroundMusic();
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
        public void BackgroundMusic()
        {
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
        }
    }
}
