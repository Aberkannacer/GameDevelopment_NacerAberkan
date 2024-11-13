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

            frameMovement += CurrentFrame.SourceRecatangle.Width * gameTime.ElapsedGameTime.TotalSeconds;
            
            if (frameMovement >= CurrentFrame.SourceRecatangle.Width/20) //20 keer sneller lopen. Je kan dit nog aanpassen
            {
                counter++;
                frameMovement = 0;
            }
            {

            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }

        }
    }
}
