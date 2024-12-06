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
        public WalkEnemy(ContentManager content, int width, int height) 
            : base(width, height)
        {
            LoadContent(content);
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
