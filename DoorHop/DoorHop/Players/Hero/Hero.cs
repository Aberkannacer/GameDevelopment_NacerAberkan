using DoorHop.Animation;
using DoorHop.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Hero
{
    public class Hero : Player
    {

        public Hero(ContentManager content) : base(content, new KeyBoardReader())
        {
            LoadContent(content);
            SetAnimationSpeed(0.6f); //om de snelheid van de animatie aan te passen
            SetMoveSpeed(3.5f);// om de snelheid van de hero aan te passen
            SetJumpForce(13f);//om de spring hoogt aan te passen
        }

        public override void SetAnimationSpeed(float speed)
        {
            animatie?.SetSpeed(speed);
        }

        protected override void LoadContent(ContentManager contentManager)
        {
            playerTexture = contentManager.Load<Texture2D>("Player");
            animatie = new Animatie(playerTexture, true);
            //heel de sprite
            animatie.AddFrames(
                row: 1,           // Tweede rij in de sprite sheet (y=64)
                frameWidth: 64,   // Breedte van elk frame
                frameHeight: 65,  // Hoogte van elk frame
                numberOfFrames: 8 // Aantal frames in de animatie
            );
            position = new Vector2(200, 200);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
                spriteBatch.Draw(playerTexture,position,animatie.CurrentFrame.SourceRecatangle,
                    Color.White,0f,Vector2.Zero,1f,SpriteEffects.None,0f);
            
        }
    }

}
