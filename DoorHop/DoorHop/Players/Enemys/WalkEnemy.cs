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
        private Animatie currentAnimation;
        private Animatie runAnimation;
        private Animatie idleAnimation;
        private bool isEnemyMoving;
        private float moveSpeed;

        public WalkEnemy(ContentManager content, int width, int height) 
            : base(null, width, height)
        {
            LoadContent(content);
            SetMoveSpeed(2f);
        }

        private void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Player2");

            runAnimation = new Animatie(texture, true);
            idleAnimation = new Animatie(texture, true);

            // Voeg verschillende animaties toe
            runAnimation.AddAnimationFrames(
                row: 1,
                frameWidth: 64,
                frameHeight: 64,
                numberOfFrames: 8);
            idleAnimation.AddAnimationFrames(
                row: 0,
                frameWidth: 64,
                frameHeight: 64,
                numberOfFrames: 4);

            //beginsituatie begint met de idle dus stilstaan
            currentAnimation = runAnimation;

            position = new Vector2(200, 200);
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (isEnemyMoving)
            {
                position.X += moveSpeed;
                if (position.X > 800)
                {
                    isEnemyMoving = false;
                }
            }
            else
            {
                position.X -= moveSpeed;
                if (position.X > 0)
                {
                    isEnemyMoving = true;
                }
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            currentAnimation = runAnimation; 
            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isAlive) return;
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.SourceRecatangle,
                        Color.White, 0f, Vector2.Zero, 1.5f, isEnemyMoving ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            
            
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public void SetAnimationSpeed(float speed)
        {
            runAnimation.SetSpeed(speed);
            idleAnimation.SetSpeed(speed);
        }


    }
}
