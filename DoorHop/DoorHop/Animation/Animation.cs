using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Animation
{
    public class Animatie
    {
        public AnimationFrame CurrentFrame { get; set; }

        private List<AnimationFrame> frames;

        private int counter;

        private double frameMovement = 0;

        private float speed = 0.1f;

        public Animatie()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame animationFrame)
        {
            frames.Add(animationFrame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            frameMovement += CurrentFrame.SourceRecatangle.Width * gameTime.ElapsedGameTime.TotalSeconds * speed;
            
            if (frameMovement >= CurrentFrame.SourceRecatangle.Width / 20)
            {
                counter++;
                frameMovement = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        public AnimationFrame GetFrame(int index)
        {
            if (index >= 0 && index < frames.Count)
            {
                return frames[index];
            }
            return frames[0]; // Retourneer het eerste frame als fallback
        }

        public void Reset()
        {
            counter = 0;
            CurrentFrame = frames[0];
            frameMovement = 0;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}
