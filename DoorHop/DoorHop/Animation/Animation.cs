using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DoorHop.Animation
{
    public class Animatie
    {
        private Dictionary<string, List<AnimationFrame>> animations;
        private string currentAnimation;
        private int counter;
        private double frameMovement = 0;
        private float speed;
        private bool isLooping;
        private Texture2D texture;

        public AnimationFrame CurrentFrame { get; private set; }

        public Animatie(Texture2D texture, bool isLooping)
        {
            animations = new Dictionary<string, List<AnimationFrame>>();
            this.texture = texture;
            this.isLooping = isLooping;
            this.speed = 1f;
        }

        public void AddAnimation(string name, int row, int frameWidth, int frameHeight, int numberOfFrames)
        {
            var frames = new List<AnimationFrame>();
            for (int i = 0; i < numberOfFrames; i++)
            {
                frames.Add(new AnimationFrame(new Rectangle(
                    i * frameWidth,
                    row * frameHeight,
                    frameWidth,
                    frameHeight
                )));
            }
            animations[name] = frames;

            if (currentAnimation == null)
            {
                Play(name);
            }
        }

        public void Play(string name)
        {
            if (!animations.ContainsKey(name))
            {
                
                return;
            }

            if (currentAnimation != name)
            {
                currentAnimation = name;
                counter = 0;
                frameMovement = 0;
                CurrentFrame = animations[name][0];
            }
        }

        public void Update(GameTime gameTime)
        {
            if (string.IsNullOrEmpty(currentAnimation)) return;

            var frames = animations[currentAnimation];
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

        public void SetSpeed(float newSpeed)
        {
            speed = 1f / newSpeed;
        }

        public void Reset()
        {
            counter = 0;
            frameMovement = 0;
            if (!string.IsNullOrEmpty(currentAnimation))
            {
                CurrentFrame = animations[currentAnimation][0];
            }
        }
    }
}
