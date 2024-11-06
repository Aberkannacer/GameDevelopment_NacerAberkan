using DoorHop.Animation;
using DoorHop.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop
{
    public class Player:IGameObject
    {
        Texture2D playerTexture;
        Animatie animatie;
        
        

        public Player(Texture2D texture)
        {
            playerTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(64, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(128, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(192, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(256, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(320, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(384, 0, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(448, 0, 64, 64)));
        }

        public void Update()
        {
            animatie.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, new Vector2(0, 0), animatie.CurrentFrame.SourceRecatangle , Color.White);

        }

    }
}
