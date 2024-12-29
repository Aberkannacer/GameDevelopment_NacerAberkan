using DoorHop.TileMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoorHop.Animation
{
    public class AnimationFrame
    {
        //rectangle & box
        public Rectangle sourceRecatangle { get; set; }
        public Rectangle nonTransparentBoundingBox { get; set; }

        public AnimationFrame(Rectangle rectangle)
        {
            sourceRecatangle = rectangle;
        }
        public void SetTexture(Texture2D texture)
        {
            if (texture != null)
            {
                nonTransparentBoundingBox = RectangleBorderHelper.GetNonTransparentBoundingBox(texture, sourceRecatangle);
            }
        }
    }
}
