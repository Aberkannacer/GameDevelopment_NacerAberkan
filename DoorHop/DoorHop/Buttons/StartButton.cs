using DoorHop.GameStates;
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
        public StartButton(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Vector2 position, string text) : base(game, graphicsDevice, content, position, text)
        {
        }
        protected override void OnClick()
        {
            game.ChangeState(new LevelState(game, content));
        }

    }
}
