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
        private float speed = 0.1f;
        private Texture2D texture;
        private float frameTime;
        private bool isLooping;

        public Animatie(Texture2D texture, bool isLooping)
        {
            frames = new List<AnimationFrame>();
            this.texture = texture;
            //this.frameTime = frameTime;
            this.isLooping = isLooping;
        }

        public void AddFrame()
        {
            frames.Add(new AnimationFrame(new Rectangle(0, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(64, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(128, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(192, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(256, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(320, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(384, 64, 64, 64)));
            frames.Add(new AnimationFrame(new Rectangle(448, 64, 64, 64)));
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
