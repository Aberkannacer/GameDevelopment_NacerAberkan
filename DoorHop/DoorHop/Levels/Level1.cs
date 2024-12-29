using DoorHop.Collectables;
using DoorHop.GameStates;
using DoorHop.Input;
using DoorHop.Players.Enemys;
using DoorHop.Players.Heros;
using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace DoorHop.Levels
{
    internal class Level1 : Level
    {
        private SpriteFont font;
        private Texture2D doorTexture;
        private Door door;


        public Door Door { get; private set; }

        public Level1(ContentManager content, Hero hero, GraphicsDevice graphicsDevice, Game1 game) : base(content, hero, graphicsDevice, game)
        {
            this.hero = hero;
            hero.position = new Vector2(25,100);

            CreateEnemies();
            

            coins.Add(new Collectable(content, new Vector2(730, 120)));
            coins.Add(new Collectable(content, new Vector2(470, 120)));
            coins.Add(new Collectable(content, new Vector2(100, 270)));

        }
        private void CreateEnemies()
        {
            enemies.Add(new WalkEnemy(content, 64, 64, new Vector2(300, 386)));
            enemies.Add(new ShootEnemy(content, 64, 64, new Vector2(565, 270)));
            enemies.Add(new GhostEnemy(content, 64, 64, new Vector2(700, 400)));
        }

        public override void Load()
        {
            base.Load();

            Tiles.Content = content;

            doorTexture = content.Load<Texture2D>("door");
            door = new Door(doorTexture, new Vector2(700, 400));

            font = content.Load<SpriteFont>("MyFont");

            map = new Map();
            levelOne = new int[,]
            {
                {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,4,4,4,4},
                {4,0,0,0,0,0,4,4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4},
                {4,0,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,1,0,0,0,0,4},
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
                
                // Ga naar Level 2
                game.ChangeState(new LevelState(game, content, 2, hero));
                
            }
            base.Update(gameTime, tiles);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            spriteBatch.DrawString(font, $"To open the door: {collectedCoins}/{totalCoins}", new Vector2(350, 40), Color.Black);
            door.Draw(spriteBatch);
            
        }

        
    }
}
