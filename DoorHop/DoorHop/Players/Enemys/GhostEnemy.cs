using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Players.Enemys
{
    internal class GhostEnemy:Enemy
    {
        private float speedGhost;
        public GhostEnemy(ContentManager content,int width, int height, Vector2  position) : base(width, height)
        {
            //enemy
            enemyHeight = 64;
            enemyWidth = 64;
            //speed
            speedGhost = 2f;
            //posiiton
            this.position = position;
            //score
            score = 100;
            LoadContent(content);
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("GhostEnemy");

            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(0, 32, 32, 4);
            currentAnimation.SetSpeed(1.0f);
        }
        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies)
        {
            Follow(hero);

            int collisionBoxWidth = enemyWidth;
            int collisionBoxHeight = enemyHeight;
            int xOffset = (enemyWidth - collisionBoxWidth);
            int yOffset = (enemyHeight - collisionBoxHeight);

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
        private void Follow(Hero hero)
        {
            Vector2 direction = hero.position - position;
            if (direction.Length() > 0)
            {
                direction.Normalize();
                //beweging naar de hero
                position += direction * speedGhost;
            }
        }
    }
}
