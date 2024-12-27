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
        private Level1 level1;
        private Level2 level2;
        private Level currentLevel;
        private int LevelWon = 0;
        private Hero hero;

        public LevelState(Game1 game, ContentManager content) : base(game, content)
        {
            if (game == null)
            {
                System.Diagnostics.Debug.WriteLine("Game is null in LevelState constructor!");
            }
            level1 = new Level1(content, new Hero(content, new KeyBoardReader(), game), game.GraphicsDevice,game);
            level2 = new Level2(content, new Hero(content, new KeyBoardReader(), game), game.GraphicsDevice, game);
            currentLevel = level1;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }

        public override void LoadContent()
        {
            currentLevel.Load();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);

            if (currentLevel is Level2 level2 && hero.Bounds.Intersects(level2.Door.Bounds))
            {
                System.Diagnostics.Debug.WriteLine("Hero has touched the door!");
                game.ChangeState(new GameWonState(game, content));
            }
        }
    }
}

