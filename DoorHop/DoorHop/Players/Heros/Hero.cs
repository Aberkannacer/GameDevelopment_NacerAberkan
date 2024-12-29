using DoorHop.Animation;
using DoorHop.Input;
using DoorHop.Players.Enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DoorHop.Players.Heros
{
    public class Hero : Player
    {
        //sprite
        private int spriteWidth;
        private int spriteHeight;
        //grafity
        private float gravity;
        private float maxFallSpeed;
        //healt & death
        private float health;
        public bool isDead;


        public Hero(ContentManager content, IInputReader inputReader, Game game, Vector2 position)
            : base(content, inputReader, game)
        {
            //positie
            this.position = position;
            //sprite
            spriteWidth = 64;
            spriteHeight = 64;
            //health
            Health = 3; //dit is een int
            health = 3; //dit is een float
            //grafity
            gravity = 0.5f;
            maxFallSpeed = 7f;

            LoadContent(content);
            UpdateBounds();
            SetMoveSpeed(3f);// Bewegingssnelheid
            SetJumpForce(-13f);// Sprongkracht
        }

        public int Health { get; private set; }
        public override void Update(GameTime gameTime, List<TileMap.CollisionTiles> tiles, Hero hero, List<Enemy> enemies)
        {
            base.Update(gameTime, tiles, hero, enemies);
            Jump();
            Grafity();
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

        public void SetJumpForce(float force)
        {
            jumpForce = force;
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public void GetHit(int damage)
        {
            if (!isInvulnerable)
            {
                health -= damage;
                isInvulnerable = true;
                invulnerabilityTimer = invulnerabilityDuration;

                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                }
            }
            Health = (int)health;
        }
        public void Jump()
        {
            if (inputReader.IsJumpKeyPressed() && isGrounded)
            {
                velocity.Y = jumpForce;
                isJumping = true;
                isGrounded = false;
            }
        }
        public void Grafity()
        {
            if (!isGrounded)
            {
                velocity.Y = Math.Min(velocity.Y + gravity, maxFallSpeed);
            }
        }

        
    }

}