using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private LevelState levelState;
        private Rectangle backgroundRect;
        private Texture2D gameWinTexture;
        private SpriteFont font;
        private Game1 game;
        private float timer; // Timer voor automatische overgang
        private const float transitionTime = 3f; // Tijd in seconden voor de overgang

        public GameWonState(Game1 game, GraphicsDevice graphicsDevice,ContentManager content) : base(game, content)
        {
            this.game = game;
            backgroundRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            Load();
        }
        public virtual void Load()
        {
            font = content.Load<SpriteFont>("MyFont"); // Zorg ervoor dat je een font hebt
            gameWinTexture = content.Load<Texture2D>("WonScreen");

        }

        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= transitionTime)
            {
                
                // Ga terug naar het menu of een andere staat
                game.ChangeState(new MenuState(game, content, game.GraphicsDevice));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameWinTexture, backgroundRect, Color.White);

        }

        public override void PostUpdate(GameTime gameTime)
        {
        }
    }
}
