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
                this.texture = content.Load<Texture2D>("Player2");
                
                animatie = new Animatie(this.texture, true);
                
                // Voeg verschillende animaties toe
                
                animatie.AddAnimation("Idle", 0, width, height, 4);
                animatie.AddAnimation("Walk", 1, width, height, 8);
                animatie.AddAnimation("Attack", 2, width, height, 12);

                SetPosition(new Vector2(320, 360));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing enemy: {ex.Message}");
            }
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (!IsAlive) return;

            try
            {
                // Standaard walking animatie
                animatie.Play("Walk");
                animatie.Update(gameTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating enemy: {ex.Message}");
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
                        Color.White,0f,Vector2.Zero,1.5f,SpriteEffects.None,0f);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error drawing enemy: {ex.Message}");
            }
        }

        
    }
}
