using DoorHop.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public WalkEnemy(ContentManager content, Texture2D texture, int row, int col, int width, int height) 
            : base(texture, row, col, width, height)
        {
            try
            {
                this.texture = content.Load<Texture2D>("EnemyWalk-Run");
                
                animatie = new Animatie(this.texture, true);
                animatie.AddFrames(
                    row: 0,
                    frameWidth: width,
                    frameHeight: height,
                    numberOfFrames: 6
                );
                //voorlopig op de grond gelegd
                SetPosition(new Vector2(320, 360));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing enemy: {ex.Message}");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive || texture == null) return;

            try
            {
                if (animatie?.CurrentFrame != null)
                {
                    spriteBatch.Draw(texture,position,animatie.CurrentFrame.SourceRecatangle,
                        Color.White,0f,Vector2.Zero,1f,SpriteEffects.None,0f
                    );
                }
                else
                {
                    spriteBatch.Draw(
                        texture,
                        new Rectangle((int)position.X, (int)position.Y, Width, Height),
                        Color.White
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error drawing enemy: {ex.Message}");
            }
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (!IsAlive) return;

            try
            {
                animatie?.Update(gameTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating enemy: {ex.Message}");
            }
        }
    }
}
