using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Players.Enemys
{
    internal class ShootEnemy : Enemy
    {
        //textures
        private List<Bullet> bullets;
        private Texture2D bulletTexture;
        //shoot
        private float shootTimer;
        private float shootInterval = 2.0f; // Interval in seconden
        //bullet
        public List<Bullet> Bullets => bullets;

        public ShootEnemy(ContentManager content, int width, int height, Vector2 position) : base(width, height)
        {
            this.position = position;
            //enemy
            enemyWidth = 64;
            enemyHeight = 64;
            //bullet
            bullets = new List<Bullet>();
            //shoot
            shootTimer = 2f;
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
            texture = content.Load<Texture2D>("ShootEnemy");
            bulletTexture = content.Load<Texture2D>("Bullet");

            //voor shootenemy
            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(0, 64, 64, 6);
            currentAnimation.SetSpeed(1.0f);

            deathAnimation = new Animatie(texture, false);
            deathAnimation.AddAnimationFrames(3, 64, 64, 8);

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
                shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (shootTimer >= shootInterval)
                {
                    Shoot(hero);
                    shootTimer = 0f;
                }
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    bullets[i].Update(gameTime, hero);

                    if (bullets[i].CollisionCheck(hero))
                    {
                        hero.GetHit(1);
                        bullets.RemoveAt(i);
                        continue;
                    }
                    if (bullets[i].IsDeleted())
                    {
                        bullets.RemoveAt(i);
                    }
                }
            }
            //collision           
            int collisionBoxWidth = enemyWidth - 4;
            int collisionBoxHeight = enemyHeight - 20;
            int xOffset = (enemyWidth - collisionBoxWidth) + 3;
            int yOffset = (enemyHeight - collisionBoxHeight) - 5;

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
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.sourceRecatangle,
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            }

            foreach (var item in bullets)
            {
                item.Draw(spriteBatch);
            }
            /*#if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
            #endif*/
        }
        public void Shoot(Hero hero)
        {
            //zorgt voor de beweging naar de hero
            Vector2 direction = (hero.position - position);
            Bullet bullet = new Bullet(bulletTexture, position, direction);
            bullets.Add(bullet);
        }
        public void RemoveBullet(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
        public override void TakeDamage()
        {
            isDead = true;
        }
    }
}