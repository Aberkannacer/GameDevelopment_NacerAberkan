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

            // Voeg verschillende animaties toe
            animatie.AddAnimation("Idle", 0, 64, 65, 8);
            animatie.AddAnimation("Walk", 1, 64, 65, 8);
            animatie.AddAnimation("Attack", 2, 64, 65, 5);
            animatie.AddAnimation("Jump", 3, 64, 65, 4);
            animatie.AddAnimation("Dead", 4, 64, 65, 6);

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
            // Bepaal welke animatie moet worden afgespeeld
            if (isJumping)
            {
                animatie.Play("Jump");
            }
            else if (isMoving)
            {
                animatie.Play("Walk");
            }
            else
            {
                animatie.Play("Idle");
            }

            animatie.Update(gameTime);
            base.Update(gameTime, tiles);
        }
    }

}
