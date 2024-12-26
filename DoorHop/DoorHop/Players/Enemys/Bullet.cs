using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    public class Bullet
    {
        public Texture2D bulletTexture;
        private Vector2 positionBullet;
        protected Rectangle bounds;
        private Animatie bulletAnimation; // Voeg animatie toe
        public int bulletSpeed = 3;
        private Vector2 directionHero;

        private const int bulletWidth = 64;
        private const int bulletHeight = 64;

        public Bullet(Texture2D texture, Vector2 startPosition)
        {
            this.positionBullet = startPosition;
            this.bulletTexture = texture;

            //voor bullet
            bulletAnimation = new Animatie(bulletTexture, true);
            bulletAnimation.AddAnimationFrames(16, 16, 16, 5);
            bulletAnimation.SetSpeed(1f);

        }


        public void Update(GameTime gameTime, Hero hero)
        {
            Vector2 direction = (hero.Position - positionBullet);
            direction.Normalize(); // Normaliseer de richting
            positionBullet += direction * bulletSpeed; // Beweeg de kogel in de richting
            bulletAnimation.Update(gameTime);

            int collisionBoxWidth = bulletWidth;
            int collisionBoxHeight = bulletHeight;
            int xOffset = (bulletWidth - collisionBoxWidth);
            int yOffset = (bulletHeight - collisionBoxHeight);

            bounds = new Rectangle(
                (int)positionBullet.X + xOffset,
                (int)positionBullet.Y + yOffset,
                collisionBoxWidth,
                collisionBoxHeight
            );
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Bepaal of de kogel naar links of rechts beweegt
            SpriteEffects effects = bulletSpeed > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            // Teken de kogel met de juiste effecten
            spriteBatch.Draw(bulletTexture, positionBullet, bulletAnimation.CurrentFrame.sourceRecatangle, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);

#if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
#endif
        }

        public bool IsDeleted()
        {
            return positionBullet.X > 800; // Verander dit naar de breedte van je scherm
        }

        public bool CollisionCheck(Hero hero)
        {
            if (hero == null) return false;
            return hero.Bounds.Intersects(new Rectangle((int)positionBullet.X, (int)positionBullet.Y, bulletTexture.Width, bulletTexture.Height));
        }

        public void SetDirection(Vector2 direction)
        {
            directionHero = direction;
        }
    }
}