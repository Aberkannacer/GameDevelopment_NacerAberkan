using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    internal class GhostEnemy:Enemy
    {
        private const int ENEMY_WIDTH = 64;
        private const int ENEMY_HEIGHT = 64;
        private float speedGhost = 2f;
        public GhostEnemy(ContentManager content,int width, int height, Vector2  position) : base(width, height)
        {
            LoadContent(content);
            this.position = position;
            //position = new Vector2(700,400);
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("GhostEnemy");

            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(0, 32, 32, 4);
            currentAnimation.SetSpeed(1.0f);
        }


        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero)
        {
            Follow(hero);

            int collisionBoxWidth = ENEMY_WIDTH;
            int collisionBoxHeight = ENEMY_HEIGHT;
            int xOffset = (ENEMY_WIDTH - collisionBoxWidth);
            int yOffset = (ENEMY_HEIGHT - collisionBoxHeight);

            bounds = new Rectangle(
                (int)position.X + xOffset,
                (int)position.Y + yOffset,
                collisionBoxWidth,
                collisionBoxHeight
            );

            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.sourceRecatangle,
                Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.FlipHorizontally, 0f);

        }

        public bool CollisionCheck(Hero hero)
        {
            if (hero == null) return false;
            return bounds.Intersects(hero.Bounds);
        }

        private void Follow(Hero hero)
        {
            Vector2 direction = hero.position - position;
            if (direction.Length() > 0)
            {
                direction.Normalize(); // Normaliseer de richting
                position += direction * speedGhost; // Beweeg naar de hero
            }
        }

        

    }
}
