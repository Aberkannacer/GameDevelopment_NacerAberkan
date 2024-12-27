using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DoorHop.Buttons;

namespace DoorHop.GameStates
{
    public abstract class State
    {
        protected ContentManager content;
        protected Game1 game;
        internal List<Button> buttons;

        public State(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
            buttons = new List<Button>();
        }
        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);
    }
}
