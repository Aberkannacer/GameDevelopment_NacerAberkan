using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Heros
{
    internal class HealthHeart
    {
        private Texture2D heartTexture;
        private Vector2 position;
        private int heartSpacing = 40;
        private Player player;

        public HealthHeart(Player player,Vector2 position)
        {
            this.player = player;
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
            heartTexture = content.Load<Texture2D>("Hearth");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < player.healthMax; i++)
            {
                Color heartColor = i < player.health ? Color.White : Color.Black;


                Vector2 hearthPosition = new Vector2(position.X + (i * heartSpacing), position.Y);

                spriteBatch.Draw(heartTexture, hearthPosition, null,heartColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
