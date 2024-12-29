using DoorHop.Animation;
using DoorHop.Players.Enemys;
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

namespace DoorHop.Collectables
{
    internal class Collectable
    {
        //textures & bound
        private Texture2D collectableTexture;
        private Rectangle bounds;
        private Vector2 positionCollectable;
        protected Animatie coinAnimation;
        //width & height
        private int collectableWidth;
        private int collectableheight;

        public Collectable(ContentManager content, Vector2 startPosition)
        {
            this.positionCollectable = startPosition;
            collectableWidth = 20;
            collectableheight = 20;
            LoadContent(content);
        }

        public void LoadContent(ContentManager content)
        {
            //texture
            collectableTexture = content.Load<Texture2D>("Coin");
            //animation coin
            coinAnimation = new Animatie(collectableTexture, true);
            coinAnimation.AddAnimationFrames(0, 20, 100, 5);
            coinAnimation.SetSpeed(1.0f);
        }

        public void Update(GameTime gameTime, Hero hero)
        {
            //update animation
            coinAnimation.Update(gameTime);

            //collision
            int collisionBoxWidth = collectableWidth;
            int collisionBoxHeight = collectableheight;
            int xOffset = (collectableWidth - collisionBoxWidth);
            int yOffset = (collectableheight - collisionBoxHeight)+40;

            bounds = new Rectangle(
                (int)positionCollectable.X + xOffset,
                (int)positionCollectable.Y + yOffset,
                collisionBoxWidth,
                collisionBoxHeight
            );
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(collectableTexture, positionCollectable, coinAnimation.CurrentFrame.sourceRecatangle, 
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            //debug
            /*#if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
            #endif
            */
        }
        public bool CollisionCheck(Hero hero)
        {
            if (hero == null) return false;
            return hero.Bounds.Intersects(new Rectangle((int)positionCollectable.X, (int)positionCollectable.Y, collectableWidth, collectableheight));
        }
    }
}
