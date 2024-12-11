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
        private const int SPRITE_WIDTH = 64;
        private const int SPRITE_HEIGHT = 64;

        public Hero(ContentManager content, IInputReader inputReader) 
            : base(content, inputReader)
        {
            LoadContent(content);
            UpdateBounds();
            
            // Start waardes instellen
            SetAnimationSpeed(1f, 1f);  // Run sneller, idle langzamer
            SetMoveSpeed(3.5f);               // Bewegingssnelheid
            SetJumpForce(-15f);             // Sprongkracht

            //voor health
            health = 5;
        }

    

        public void SetJumpForce(float force)
        {
            jumpForce = force;  // Gebruikt nu de protected variable van Player
        }

        protected override void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player");
            
            runAnimation = new Animatie(playerTexture, true);
            runAnimation.AddAnimationFrames(row: 1, frameWidth: 64, frameHeight: 64, numberOfFrames: 8);
            
            idleAnimation = new Animatie(playerTexture, true);
            idleAnimation.AddAnimationFrames(row: 0, frameWidth: 64, frameHeight: 64, numberOfFrames: 4);

            attackAnimation = new Animatie(playerTexture, true);
            attackAnimation.AddAnimationFrames(row: 2, frameWidth: 64, frameHeight: 64, numberOfFrames: 4);

            currentAnimation = idleAnimation;
        }

        protected override void UpdateBounds()
        {
            int boundsWidth = (int)(SPRITE_WIDTH * 0.8f);
            int boundsHeight = SPRITE_HEIGHT;
            int boundsX = (int)(position.X + (SPRITE_WIDTH - boundsWidth) / 2);
            int boundsY = (int)position.Y;

            bounds = new Rectangle(boundsX, boundsY, boundsWidth, boundsHeight);
        }

        protected virtual void UpdateAnimation(GameTime gameTime)
        {
            // Kies animatie gebaseerd op beweging
            // Als de hero beweegt dan veranderd de animatie naar lopen
            currentAnimation = isMoving ? runAnimation : idleAnimation;
            //als de hero gaat aanvallen dan gaat de hero de animatie van attacken tonen
            currentAnimation = isAttacking ? attackAnimation : idleAnimation;

            //NOG LATEN WERKEN DAT JE KAN KLIKKEN
            
            // Update huidige animatie frame
            currentAnimation.Update(gameTime);
        }

        public void SetAnimationSpeed(float runSpeed, float idleSpeed)
        {
            runAnimation?.SetSpeed(runSpeed);
            idleAnimation?.SetSpeed(idleSpeed);
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        private void Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
            }
        }
    }

}
