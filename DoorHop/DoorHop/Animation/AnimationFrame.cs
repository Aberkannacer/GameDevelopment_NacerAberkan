using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.Animation
{
    public class AnimationFrame
    {
        public Rectangle sourceRecatangle { get; set; }
        public Rectangle nonTransparentBoundingBox { get; set; }
        public Texture2D texture { get; set; }

        public AnimationFrame(Rectangle rectangle)
        {
            sourceRecatangle = rectangle;
            nonTransparentBoundingBox = RectangleBorderHelper.GetNonTransparentBoundingBox(texture, sourceRecatangle);
        }
    }
}
