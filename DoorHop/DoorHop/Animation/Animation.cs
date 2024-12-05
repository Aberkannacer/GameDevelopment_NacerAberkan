using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace DoorHop.Animation
{

    public class Animatie
    {
        private List<AnimationFrame> animations;
        private int currentAnimation;
        private int counter;
        private float timer;
        private float speed;
        private bool isLooping;
        private Texture2D texture;

        public AnimationFrame CurrentFrame { get; private set; }

        public Animatie(Texture2D texture, bool isLooping)
        {
            animations = new List<AnimationFrame>();
            this.texture = texture;
            this.isLooping = isLooping;
            this.speed = 1f;
            currentAnimation = 0;
            timer = 0;
        }

        public void AddAnimationFrames(int row, int frameWidth, int frameHeight, int numberOfFrames)
        {
            animations.Clear();

            
            for (int i = 0; i < numberOfFrames; i++)
            {
                animations.Add(new AnimationFrame(new Rectangle(
                    i * frameWidth,
                    row * frameHeight,
                    frameWidth,
                    frameHeight
                )));
            }
            CurrentFrame = animations[0];

            
        }


        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= 0.1f * speed)
            {
                currentAnimation++;
                timer = 0;

                if (currentAnimation >= animations.Count)
                {
                    currentAnimation = isLooping ? 0 : animations.Count - 1;
                }

                CurrentFrame = animations[currentAnimation];
            }
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        public void Reset()
        {
            currentAnimation = 0;
            CurrentFrame = animations[0];
            timer = 0;
        }
    }
}
