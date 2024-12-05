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
        private Animatie currentAnimation;
        private Animatie runAnimation;
        private Animatie idleAnimation;

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
            runAnimation = new Animatie(playerTexture, true);
            idleAnimation = new Animatie(playerTexture, true);

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
                numberOfFrames: 8);

            //beginsituatie begint met de idle dus stilstaan
            currentAnimation = idleAnimation;

            position = new Vector2(200, 200);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (animatie?.CurrentFrame != null)
            {
                // Teken de sprite
                spriteBatch.Draw(playerTexture,position,animatie.CurrentFrame.SourceRecatangle,
                    Color.White,0f,Vector2.Zero,0.7f,SpriteEffects.None,0f);

                // Debug: teken de collision box (rood transparant vierkant)
                #if DEBUG
                Texture2D debugTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                debugTexture.SetData(new[] { Color.White });
                spriteBatch.Draw(debugTexture, CollisionBox, Color.Red * 0.3f);
                #endif
            }
        }

        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles)
        {
            if (isMoving)
            {
                currentAnimation = runAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }



            currentAnimation.Update(gameTime);
            base.Update(gameTime, tiles);
        }
    }

}
