using DoorHop.Levels;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DoorHop.GameStates
{
    internal class GameWonState : State
    {
        private Hero hero;
        private Rectangle backgroundRect;
        private Texture2D gameWinTexture;
        private SpriteFont font;
        private Game1 game;
        private float timer;
        private float transitionTime;
        private Song backgroundMusic;

        public GameWonState(Game1 game, GraphicsDevice graphicsDevice,ContentManager content, Hero hero) : base(game, content)
        {
            this.hero = hero;
            this.game = game;
            transitionTime = 5.5f;

            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            
            Load();
        }
        public virtual void Load()
        {
            font = content.Load<SpriteFont>("MyFont");
            gameWinTexture = content.Load<Texture2D>("WonScreen");
            
        }

        public override void LoadContent()
        {
            backgroundMusic = content.Load<Song>("BackgroundMusic");
            
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= transitionTime)
            {
                BackgroundMusic();
                //terug naar menu
                game.ChangeState(new MenuState(game, content, game.GraphicsDevice));
                
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameWinTexture, backgroundRect, Color.White);
            spriteBatch.DrawString(font, $"Total Score: {hero.TotalScore}", new Vector2(350, 20), Color.White);
            
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }
        public void BackgroundMusic()
        {
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
        }
    }
}
