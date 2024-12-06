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
            SetAnimationSpeed(0.6f, 0.4f); ; //om de snelheid van de animatie aan te passen
            SetMoveSpeed(3.5f);// om de snelheid van de hero aan te passen
            SetJumpForce(13f);//om de spring hoogt aan te passen
        }

        public override void SetAnimationSpeed(float speedRun, float speedIdle)
        {
            runAnimation.SetSpeed(speedRun);
            idleAnimation.SetSpeed(speedIdle);
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
            if (currentAnimation.CurrentFrame != null)
            {
                // Teken de sprite
                spriteBatch.Draw(playerTexture,position, currentAnimation.CurrentFrame.SourceRecatangle,
                    Color.White,0f,Vector2.Zero,0.7f,SpriteEffects.None,0f);

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
