using DoorHop.Animation;
using DoorHop.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Players.Heros
{
    public class Hero : Player
    {
        private const int SPRITE_WIDTH = 64;
        private const int SPRITE_HEIGHT = 64;
        public Vector2 Position { get; set; }
        public int Health { get; private set; }
        public Texture2D Texture { get; set; }

        public Hero(ContentManager content, IInputReader inputReader, Game game)
            : base(content, inputReader, game)
        {
            Health = 3;
            health = 3;
            healthMax = 3;
            playerTexture = Texture;
            LoadContent(content);
            UpdateBounds();

            // Start waardes instellen
            SetAnimationSpeed(1f, 1f);  // Run sneller, idle langzamer
            SetMoveSpeed(3.5f);               // Bewegingssnelheid
            SetJumpForce(-15f);             // Sprongkracht
        }


        public void SetJumpForce(float force)
        {
            jumpForce = force;
        }

        protected override void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player");

            idleAnimation = new Animatie(playerTexture, true);
            idleAnimation.AddAnimationFrames(0, 64, 64, 8);

            runAnimation = new Animatie(playerTexture, true);
            runAnimation.AddAnimationFrames(1, 64, 64, 8);

            attackAnimation = new Animatie(playerTexture, false);
            attackAnimation.AddAnimationFrames(2, 64, 64, 5);
            attackAnimation.SetSpeed(1f);

            jumpAnimation = new Animatie(playerTexture, false);
            jumpAnimation.AddAnimationFrames(3, 64, 64, 4);

            dieAnimation = new Animatie(playerTexture, false);
            dieAnimation.AddAnimationFrames(4, 64, 64, 6);

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

        protected override void UpdateAnimation(GameTime gameTime)
        {
            if (isAttacking)
            {
                currentAnimation = attackAnimation;
                if (currentAnimation.IsAnimationFinished())
                {
                    isAttacking = false;
                }
            }
            else if (isJumping)
            {
                currentAnimation = jumpAnimation;
            }
            else if (isMoving)
            {
                currentAnimation = runAnimation;
            }
            else if (isDead)
            {
                currentAnimation = dieAnimation;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

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

        public override void GetHit(int damage)
        {
            base.GetHit(damage);
            Health = (int)health;
        }
    }

}