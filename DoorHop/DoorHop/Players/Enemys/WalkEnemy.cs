using DoorHop.Animation;
using DoorHop.Players.Heros;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Enemys
{
    internal class WalkEnemy : Enemy
    {
        private const int ENEMY_WIDTH = 38;
        private const int ENEMY_HEIGHT = 64;
        public WalkEnemy(ContentManager content, int width, int height, Vector2 position):base(width, height)
        {
            this.position = position;
            //position = new Vector2(300, 386);
            moveSpeed = 1.5f;

            LoadContent(content);
            
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Player2");

            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(1, 64, 32, 8);
            currentAnimation.SetSpeed(1.0f);
        }
        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies)
        {
            position.X += moveSpeed;
            if (position.X > 700 || position.X < 0) //loopt 420 naar positie x rechts en keert terug als die naar links gaat 0
            {
                moveSpeed = -moveSpeed;
            }

            // Update bounds met dezelfde offsets als in de constructor
            int collisionBoxWidth = ENEMY_WIDTH;
            int collisionBoxHeight = ENEMY_HEIGHT/2;
            int xOffset;
            if (moveSpeed > 0)
            {
                xOffset = (ENEMY_WIDTH - collisionBoxWidth) + 37;
            }
            else
            {
                xOffset = ENEMY_WIDTH+16;
            }
            
            int yOffset = (ENEMY_HEIGHT - collisionBoxHeight);

            bounds = new Rectangle(
                (int)position.X + xOffset,
                (int)position.Y + yOffset,
                collisionBoxWidth,
                collisionBoxHeight
            );
            //hero.JumpOnEnemy(this);
            currentAnimation?.Update(gameTime);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentAnimation?.CurrentFrame.sourceRecatangle,
                Color.White, 0f, Vector2.Zero, 2f,moveSpeed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            // Debug collision bounds
#if DEBUG
            var boundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            boundTexture.SetData(new[] { Color.Yellow * 1f });
            spriteBatch.Draw(boundTexture, bounds, Color.Yellow * 0.5f);
#endif

            
        }

        

        


    }
}
