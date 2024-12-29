using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DoorHop.Animation
{

    public class Animatie
    {
        //textures
        private Texture2D texture;
        private List<AnimationFrame> animations;
        private int currentAnimation;
        //timer & speed
        private float timer;
        private float speed;
        private bool isLooping;

        public AnimationFrame CurrentFrame { get; private set; }

        public Animatie(Texture2D texture, bool isLooping)
        {
            this.texture = texture;
            animations = new List<AnimationFrame>();
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
                var frame = new AnimationFrame(new Rectangle(
                    i * frameWidth,
                    row * frameHeight,
                    frameWidth,
                    frameHeight
                ));
                frame.SetTexture(texture);
                animations.Add(frame);
            }

            CurrentFrame = animations[0];
        }
        public void Reset()
        {
            currentAnimation = 0;
            timer = 0;
            CurrentFrame = animations[0];
        }
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timer >= 0.1f * speed)
            {
                timer = 0;
                
                if (!isLooping && currentAnimation >= animations.Count - 1)
                {
                    // Blijf op het laatste frame voor non-looping animaties
                    return;
                }
                // Update naar het volgende frame
                currentAnimation++;
                if (isLooping)
                {
                    currentAnimation %= animations.Count;
                }
                else
                {
                    currentAnimation = Math.Min(currentAnimation, animations.Count - 1);
                }

                CurrentFrame = animations[currentAnimation];
            }
        }
        public void SetSpeed(float newSpeed) => speed = newSpeed;
        public bool IsAnimationFinished()
        {
            return !isLooping && currentAnimation >= animations.Count - 1;
        }
    }
}
