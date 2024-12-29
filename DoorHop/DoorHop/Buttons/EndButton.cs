using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DoorHop.Buttons
{
    internal class EndButton : Button
    {
        public EndButton(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, Vector2 position, string text) : base(game, graphicsDevice, content, position, text)
        {
        }
        protected override void OnClick()
        {
            game.Exit();
            Environment.Exit(0);
        }
    }
}
