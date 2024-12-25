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
    internal class Bullet
    {
        public Texture2D bulletTexture;
        private Vector2 positionBullet;

        public int bulletSpeed = 5;

        public Bullet(Texture2D texture, Vector2 startPosition)
        {
            this.positionBullet = startPosition;
            this.bulletTexture = texture;
        }


        public void Update(GameTime gametime)
        {
            positionBullet.X += bulletSpeed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletTexture, positionBullet, Color.White);
        }

        public bool IsDeleted()
        {
            return positionBullet.X > 800; // Verander dit naar de breedte van je scherm
        }
    }
}
