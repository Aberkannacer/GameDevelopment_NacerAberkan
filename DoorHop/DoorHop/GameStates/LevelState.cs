using DoorHop.Collectables;
using DoorHop.Input;
using DoorHop.Levels;
using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.GameStates
{
    internal class LevelState:State
    {
        private Level1 currentLevel;

        public LevelState(Game1 game, ContentManager content) : base(game, content)
        {
            currentLevel = new Level1(content, new Hero(content, new KeyBoardReader(), game), game.GraphicsDevice);
        }

        public override void LoadContent()
        {
            currentLevel.Load();
        }

        public override void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }
    }
}

