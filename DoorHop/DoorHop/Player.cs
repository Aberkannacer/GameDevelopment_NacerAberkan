using DoorHop.Animation;
using DoorHop.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop
{
    public class Player:IGameObject
    {
        Texture2D playerTexture;
        Animatie animatie;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 acceleration;
        private Vector2 mouseVector;
        

        public Player(Texture2D texture)
        {
            playerTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(64, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(128, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(192, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(256, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(320, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(384, 64, 64, 64)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(448, 64, 64, 64)));
            position = new Vector2(10, 10);
            speed = new Vector2(1, 1);
            acceleration = new Vector2(0.1f, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
            Move(GetMouseState());
            animatie.Update(gameTime);
        }

        private Vector2 GetMouseState()
        {
            MouseState state = Mouse.GetState();
            mouseVector = new Vector2(state.X, state.Y);
            return mouseVector;
        }

        private void Move(Vector2 mouse)
        {
            var direction = Vector2.Add(mouse, -position);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 1f);// adding speed to follow mouse

            position += direction;
            speed += acceleration;
            speed = Limit(speed, 10);
            
            float tmp = speed.Length();
            
            if (position.X > 600 || position.X <0)
            {
                speed.X *= -1;
                acceleration.X *= -1;
            }
            if (position.Y > 480 || position.Y < 0)
            {
                speed.Y *= -1;
                acceleration *= -1;
            }
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position, animatie.CurrentFrame.SourceRecatangle , Color.White, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);

        }

    }
}
