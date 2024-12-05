using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Animation
{
    public class Animatie
    {
        public AnimationFrame CurrentFrame { get; private set; }
        private List<AnimationFrame> frames;
        private int counter;
        private double frameMovement = 0;
        private float speed;
        private Texture2D texture;
        private bool isLooping;

        public Animatie(Texture2D texture, bool isLooping)
        {
            frames = new List<AnimationFrame>();
            this.texture = texture;
            this.isLooping = isLooping;
            this.speed = 1f;
        }

        public void AddFrames(int row, int frameWidth, int frameHeight, int numberOfFrames)
        {
            frames.Clear();
            
            for (int i = 0; i < numberOfFrames; i++)
            {
                frames.Add(new AnimationFrame(new Rectangle(
                    i * frameWidth,
                    row * frameHeight,
                    frameWidth,
                    frameHeight
                )));
            }

            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];
            frameMovement += gameTime.ElapsedGameTime.TotalSeconds * speed;

            if (frameMovement >= 0.1f)
            {
                counter++;
                frameMovement = 0;

                if (counter >= frames.Count)
                {
                    if (isLooping)
                        counter = 0;
                    else
                        counter = frames.Count - 1;
                }
            }
        }

        public void Reset()
        {
            counter = 0;
            CurrentFrame = frames[0];
            frameMovement = 0;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = 1f / newSpeed;
            
        }
    }
}
