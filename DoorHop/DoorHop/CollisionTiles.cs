using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop
{
    public class CollisionTiles
    {
        public Rectangle Rectangle { get; private set; }

        public CollisionTiles(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }
    }
} 