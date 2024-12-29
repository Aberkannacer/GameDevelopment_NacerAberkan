using DoorHop.Animation;
using DoorHop.Input;
using DoorHop.Players.Enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DoorHop.Players.Heros
{
    public class Hero : Player
    {
        private int spriteWidth;
        private int spriteHeight;
        public int Health { get; private set; }

        public Hero(ContentManager content, IInputReader inputReader, Game game, Vector2 position)
            : base(content, inputReader, game)
        {
            this.position = position;
            spriteWidth = 64;
            spriteHeight = 64;
            Health = 3; //dit is een int
            health = 3; //dit is een float
            healthMax = 3;
            LoadContent(content);
            UpdateBounds();
            SetMoveSpeed(3f);// Bewegingssnelheid
            SetJumpForce(-15f);// Sprongkracht
        }


        public void SetJumpForce(float force)
        {
            jumpForce = force;
        }

        public override void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player");

            idleAnimation = new Animatie(playerTexture, true);
            idleAnimation.AddAnimationFrames(0, 64, 64, 8);

            runAnimation = new Animatie(playerTexture, true);
            runAnimation.AddAnimationFrames(1, 64, 64, 8);

            attackAnimation = new Animatie(playerTexture, false);
            attackAnimation.AddAnimationFrames(2, 64, 64, 5);
            attackAnimation.SetSpeed(0.8f);

            jumpAnimation = new Animatie(playerTexture, false);
            jumpAnimation.AddAnimationFrames(3, 64, 64, 4);

            dieAnimation = new Animatie(playerTexture, false);
            dieAnimation.AddAnimationFrames(4, 64, 64, 6);

            currentAnimation = idleAnimation;
        }

        protected override void UpdateBounds()
        {
            int boundsWidth = (int)(spriteWidth * 0.8f);
            int boundsHeight = spriteHeight;
            int boundsX = (int)(position.X + (spriteWidth - boundsWidth) / 2);
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


        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }


        public override void GetHit(int damage)
        {
            base.GetHit(damage);
            Health = (int)health;
        }

        

    }

}