using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DoorHop.TileMap
{
    internal class RectangleBorderHelper
    {
        public static Rectangle GetNonTransparentBoundingBox(Texture2D texture, Rectangle sourceRectangle) 
        {
            Color[] textureData = new Color[sourceRectangle.Width * sourceRectangle.Height];
            texture.GetData(0, sourceRectangle, textureData, 0, textureData.Length);

            int left = sourceRectangle.Width;
            int right = 0;
            int top = sourceRectangle.Height;
            int bottom = 0;

            for (int y = 0; y < sourceRectangle.Height; y++)
            {
                for (int x = 0; x < sourceRectangle.Width; x++)
                {
                    Color pixel = textureData[y * sourceRectangle.Width + x];
                    if (pixel.A != 0)
                    {
                        left = Math.Min(left, x);
                        right = Math.Max(right, x);
                        top = Math.Min(top, y);
                        bottom = Math.Max(bottom, y);
                    }
                }
            }
            if (left > right || top > bottom)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            return new Rectangle(left, top, right - left + 1, bottom - top + 1);
        }
    }
}
