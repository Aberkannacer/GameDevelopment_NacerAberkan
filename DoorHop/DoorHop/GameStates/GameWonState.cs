using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.GameStates
{
    internal class GameWonState : State
    {
        private SpriteFont font;
        private Game1 game;
        private float timer; // Timer voor automatische overgang
        private const float transitionTime = 3f; // Tijd in seconden voor de overgang

        public GameWonState(Game1 game, ContentManager content) : base(game, content)
        {
            this.game = game;
        }

        public override void LoadContent()
        {
            font = content.Load<SpriteFont>("MyFont"); // Zorg ervoor dat je een font hebt
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
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Congratulations! You Won!", new Vector2(200, 200), Color.White);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
