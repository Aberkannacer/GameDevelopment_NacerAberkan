using DoorHop.GameStates;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Buttons
{
    internal class StartButton : Button
    {
        private Hero hero;
        public StartButton(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Vector2 position, string text, Hero hero) : base(game, graphicsDevice, content, position, text)
        {
            this.hero = hero;
        }
        protected override void OnClick()
        {
            hero.Reset();
            game.ChangeState(new LevelState(game, content, 1, hero));
        }

    }
}
