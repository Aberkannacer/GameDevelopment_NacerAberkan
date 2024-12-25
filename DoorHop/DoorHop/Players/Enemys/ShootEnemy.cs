using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    internal class ShootEnemy : Enemy
    {
        private List<Bullet> bullets;
        private Texture2D bulletTexture;
        private const int ENEMY_WIDTH = 64;
        private const int ENEMY_HEIGHT = 64;

        public ShootEnemy(ContentManager content, int width, int height) : base(width, height)
        {
            LoadContent(content);
            position = new Vector2(740, 240);
            bullets = new List<Bullet>();
        }
        private void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ShootEnemy");
            bulletTexture = content.Load<Texture2D>("Bullet");

            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(0, 64, 64, 6);
            currentAnimation.SetSpeed(1.0f);
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            //om de 2 seconden schiet de enemy een bullet af
            if (gameTime.TotalGameTime.Seconds % 2 ==0)
            {
                Shoot();
            }
            // Update bounds met dezelfde offsets als in de constructor
            int collisionBoxWidth = ENEMY_WIDTH-5;
            int collisionBoxHeight = ENEMY_HEIGHT-20;
            int xOffset = (ENEMY_WIDTH - collisionBoxWidth)+5;
            int yOffset = (ENEMY_HEIGHT - collisionBoxHeight)-5;

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
                Color.White, 0f, Vector2.Zero, 1f, moveSpeed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            foreach (var item in bullets)
            {
                item.Draw(spriteBatch);
            }

#if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
#endif
        }

        private void Shoot()
        {
            Bullet bullet = new Bullet(bulletTexture, position);
            bullets.Add(bullet);
        }

        

        public bool CollisionCheck(Hero hero)
        {
            if (hero == null) return false;
            return bounds.Intersects(hero.Bounds);
        }


    }
}
