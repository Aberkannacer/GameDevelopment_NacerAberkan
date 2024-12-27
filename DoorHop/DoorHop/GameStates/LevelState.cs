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

        public LevelState(Game1 game, ContentManager content,int levelNumber) : base(game, content)
        {
            hero = new Hero(content, new KeyBoardReader(), game);
            if (game == null)
            {
                System.Diagnostics.Debug.WriteLine("Game is null in LevelState constructor!");
            }
            if (levelNumber == 1)
            {
                currentLevel = new Level1(content, hero, game.GraphicsDevice, game);
            }
            else if (levelNumber == 2)
            {
                currentLevel = new Level2(content, hero, game.GraphicsDevice, game);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }

        public override void LoadContent()
        {
            if (currentLevel != null)
            {
                currentLevel.Load(); // Zorg ervoor dat currentLevel niet null is
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("currentLevel is null in LoadContent!");
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);

            if (currentLevel is Level2 level2 && hero != null && level2.Door != null && hero.Bounds.Intersects(level2.Door.Bounds))
            {
                System.Diagnostics.Debug.WriteLine("Hero has touched the door!");
                game.ChangeState(new GameWonState(game, content));
            }
        }
    }
}

