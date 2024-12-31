using DoorHop.Factory;
using DoorHop.GameStates;
using DoorHop.Interfaces;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Levels
{
    internal class Level2 : Level
    {
        //font
        private SpriteFont font;
        //door
        private Texture2D doorTexture;
        private Door door;
        //factory pattern
        private readonly IGameObjectFactory gameObjectFactory;

        public Level2(ContentManager content, Hero hero, GraphicsDevice graphicsDevice, Game1 game) : base(content, hero, graphicsDevice, game)
        {
            this.hero = hero;
            hero.position = new Vector2(80, 80);
            //gameobject
            gameObjectFactory = new ObjectFactory();
            //enemies
            CreateEnemies();
            //coins
            CreateCoins();
        }
        private void CreateEnemies()
        {
            enemies.Add(gameObjectFactory.CreateEnemy("walkEnemy", content, new Vector2(300, 386)));
            enemies.Add(gameObjectFactory.CreateEnemy("shootEnemy", content, new Vector2(355, 240)));
            enemies.Add(gameObjectFactory.CreateEnemy("ghostEnemy", content, new Vector2(700, 350)));
        }
        private void CreateCoins()
        {
            coins.Add(gameObjectFactory.CreateCoin(new Vector2(620, 250), content));
            coins.Add(gameObjectFactory.CreateCoin(new Vector2(215, 60), content));
            coins.Add(gameObjectFactory.CreateCoin(new Vector2(490, 50), content));
        }
        public override void Load()
        {
            base.Load();
            //tiles
            Tiles.Content = content;
            //door
            doorTexture = content.Load<Texture2D>("door");
            door = new Door(doorTexture, new Vector2(70, 400));
            //font
            font = content.Load<SpriteFont>("MyFont");
            //map
            map = new Map();
            levelOne = new int[,]
            {
                {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0},
                {4,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0},
                {4,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4},
                {4,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,6,0,5,0,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,6,6,5,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,6,6,6,0,0,0,0,4,4,4,4,0,0,0,0,4,4,4,4,4,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,4,4,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,4,4,4,4,4,0,0,0,0,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,4},
                {4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,4},
                {4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            };
            map.Generate(levelOne, 30);
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            CheckEnemyCollisions();
            if (hero.Bounds.Intersects(door.Bounds))
            {
                hero.WinSound();
                hero.ResetLevel2();
                
                // gewonnen
                game.ChangeState(new GameWonState(game,graphicsDevice ,game.Content, hero));
            }
            base.Update(gameTime, tiles);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, $"To open the door: {collectedCoins}/{totalCoins}", new Vector2(350, 40), Color.Black);
            map.Draw(spriteBatch);
            door.Draw(spriteBatch);
        }
    }
}
