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
    internal class Level2 : Level
    {
        private WalkEnemy walkEnemy;
        private ShootEnemy shootEnemy;
        private GhostEnemy ghostEnemy;


        private SpriteFont font;

        private HealthHeart healthHeart;

        private Texture2D doorTexture;
        private Door door;
        
        public Door Door { get; private set; }

        public Level2(ContentManager content, Hero hero, GraphicsDevice graphicsDevice, Game1 game) : base(content, hero, graphicsDevice, game)
        {
            this.hero = hero;
            hero.position = new Vector2(200, 200);

            walkEnemy = new WalkEnemy(content, 64, 64, new Vector2(300, 386));
            shootEnemy = new ShootEnemy(content, 64, 64, new Vector2(325, 240));
            ghostEnemy = new GhostEnemy(content, 64, 64, new Vector2(700, 400));


            Enemies.Add(walkEnemy);
            Enemies.Add(shootEnemy);
            Enemies.Add(ghostEnemy);

            coins.Add(new Collectable(content, new Vector2(680, 200)));
            coins.Add(new Collectable(content, new Vector2(440, 100)));
            coins.Add(new Collectable(content, new Vector2(400, 270)));

            healthHeart = new HealthHeart(content, hero, new Vector2(670, 10));

            //Door = new Door(doorTexture, new Vector2(700, 400));
        }


        public override void Load()
        {
            base.Load();

            Tiles.Content = content;

            doorTexture = content.Load<Texture2D>("door"); // Zorg ervoor dat je een deur texture hebt
            door = new Door(doorTexture, new Vector2(70, 400));

            font = content.Load<SpriteFont>("MyFont");

            map = new Map();
            levelOne = new int[,]
            {
                {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,4,4,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,4,4,4,4,4,0,0,0,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,4},
                {4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {4,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            };
            map.Generate(levelOne, 30);


        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {

            if (hero.Bounds.Intersects(door.Bounds))
            {
                // Ga naar Level 2
                game.ChangeState(new GameWonState(game,graphicsDevice ,game.Content));
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
