using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Players.Enemys
{
    internal class WalkEnemy : Enemy
    {
        public WalkEnemy(ContentManager content, int width, int height, Vector2 position) :base(width, height)
        {
            this.position = position;
            //enemy
            enemyHeight = 64;
            enemyWidth = 38;
            //speed
            moveSpeed = 1.5f;
            //death
            isDead = false;
            deathAnimationTimer = 0;
            deathAnimationDuration = 0.5f;
            //score
            score = 100;
            LoadContent(content);
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Player2");

            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(1, 64, 32, 8);
            currentAnimation.SetSpeed(1.0f);

            deathAnimation = new Animatie(texture, false);
            deathAnimation.AddAnimationFrames(4, 64, 32, 5);
        }
        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies)
        {
            //regelen van de dood animatie
            if (isDead)
            {
                deathAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deathAnimationTimer >= deathAnimationDuration)
                {
                    isAlive = false;
                    return;
                }
            }
            else
            {
                position.X += moveSpeed;
                if (position.X > 700 || position.X < 0) //loopt 420 naar positie x rechts en keert terug als die naar links gaat 0
                {
                    moveSpeed = -moveSpeed;
                }
            }
            //collision
            int collisionBoxWidth = enemyWidth;
            int collisionBoxHeight = enemyHeight / 2;
            int xOffset;
            if (moveSpeed > 0)
            {
                xOffset = (enemyWidth - collisionBoxWidth) + 37;
            }
            else
            {
                xOffset = enemyWidth + 16;
            }
            int yOffset = (enemyHeight - collisionBoxHeight);

            bounds = new Rectangle(
                (int)position.X + xOffset,
                (int)position.Y + yOffset,
                collisionBoxWidth,
                collisionBoxHeight
            );
            currentAnimation.Update(gameTime);
            deathAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //als dood is dan teken je de doodanimatie
            if (isDead)
            {
                spriteBatch.Draw(texture, position, deathAnimation.CurrentFrame.sourceRecatangle,
                Color.White, 0f, Vector2.Zero, 2f, moveSpeed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, position, currentAnimation?.CurrentFrame.sourceRecatangle,
                    Color.White, 0f, Vector2.Zero, 2f, moveSpeed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }

            // Debug collision bounds
            /*#if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
            #endif*/
        }
        public override void TakeDamage()
        {
            isDead = true;
        }
    }
}
