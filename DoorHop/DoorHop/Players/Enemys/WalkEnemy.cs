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
        private float distanceToWalk;
        private float speed;

        public WalkEnemy(ContentManager content,Texture2D texture, int row, int col, int width, int height) : base(texture, row, col, width, height)
        {
            texture = content.Load<Texture2D>("EnemyWalk-Run");
            animatie = new Animatie(texture, true);
            animatie.AddFrames(
                row: 0,           // Eerste rij in de sprite sheet
                frameWidth: 64,   // Pas aan naar de juiste frame grootte
                frameHeight: 64,  // Pas aan naar de juiste frame grootte
                numberOfFrames: 6 // Aantal frames in de enemy animatie
            );
        }

        public void SetPosisition(Vector2 newPosition)
        {
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public override Rectangle HitBox 
        {
            get { return base.HitBox; }
            protected set { base.HitBox = value; } 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture, position, animatie.CurrentFrame.SourceRecatangle,
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            animatie.Update(gameTime);
            
        }
    }
}
