using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoorHop.Players.Enemys
{
    public class Bullet
    {
        //textures
        public Texture2D bulletTexture;
        private Animatie bulletAnimation;
        //bound position
        private Vector2 positionBullet;
        protected Rectangle bounds;
        //bullet
        public int bulletSpeed = 3;
        //hero
        private Vector2 directionHero;
        //width&height
        private int bulletWidth;
        private int bulletHeight;

        public Bullet(Texture2D texture, Vector2 startPosition, Vector2 direction)
        {
            //bullet
            this.positionBullet = startPosition;
            this.bulletTexture = texture;
            bulletWidth = 16;
            bulletHeight = 16;
            //hero
            this.directionHero = direction;
            this.directionHero.Normalize();
            //animatie voor bullet
            bulletAnimation = new Animatie(bulletTexture, true);
            bulletAnimation.AddAnimationFrames(16, 16, 16, 5);
            bulletAnimation.SetSpeed(1f);
        }
        public void Update(GameTime gameTime, Hero hero)
        {
            positionBullet += directionHero * bulletSpeed;
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
            SpriteEffects effects = bulletSpeed > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(bulletTexture, positionBullet, bulletAnimation.CurrentFrame.sourceRecatangle, 
                Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
            //debug
            /*
            #if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
            #endif*/
        }
        public bool IsDeleted()
        {
            //hier wordt de bullet verwijderd naarmate hij naar de breedte van de scherm gaat 
            //deze is ingesteld in de game klasse
            return positionBullet.X < 0 || positionBullet.X > 800;
        }
        public bool CollisionCheck(Hero hero)
        {
            if (hero == null) return false;
            return hero.Bounds.Intersects(new Rectangle((int)positionBullet.X, (int)positionBullet.Y, bulletWidth, bulletHeight));
        }
    }
}