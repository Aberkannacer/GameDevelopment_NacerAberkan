using DoorHop.Animation;
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
        public WalkEnemy(ContentManager content, int width, int height):base(width, height)
        {
            LoadContent(content);
            position = new Vector2(400, 386);
            moveSpeed = 1.5f;
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            position.X += moveSpeed;
            if (position.X > 560 || position.X < 0) //loopt 560 naar positie x rechts en keert terug als die naar links gaat: 0
            {
                moveSpeed = -moveSpeed;
            }
            bounds = new Rectangle((int)position.X, (int)position.Y, bounds.Width, bounds.Height);
            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.SourceRecatangle,
                Color.White, 0f, Vector2.Zero, 2f,
                moveSpeed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);


        }

        private void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Player2");
            
            currentAnimation = new Animatie(texture, true);
            currentAnimation.AddAnimationFrames(
                row: 1,
                frameWidth: 64,
                frameHeight: 32,
                numberOfFrames: 8
            );
            
            currentAnimation.SetSpeed(1.0f);
        }
    }
}
