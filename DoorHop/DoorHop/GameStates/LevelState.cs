using DoorHop.Collectables;
using DoorHop.Input;
using DoorHop.Levels;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.GameStates
{
    internal class LevelState:State
    {
        //level
        private Level currentLevel;
        //tiles
        private List<TileMap.CollisionTiles> tiles;

        public LevelState(Game1 game, ContentManager content,int levelNumber, Hero hero) : base(game, content)
        {
            if (levelNumber == 1)
            {
                currentLevel = new Level1(content, hero, game.GraphicsDevice, game);
            }
            else if (levelNumber == 2)
            {
                currentLevel = new Level2(content, hero, game.GraphicsDevice, game);
            }
            tiles = new List<TileMap.CollisionTiles>();
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
            tiles = new List<TileMap.CollisionTiles>();
        }
        public override void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime, tiles);
        }
    }
}

