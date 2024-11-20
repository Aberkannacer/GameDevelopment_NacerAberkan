using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorHop.TileMap
{
    class Map
    {
        private int width, height;
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }
        
        public int Width 
        {
            get { return width; } 
        }
        public int Height 
        {
            get { return height; } 
        }

        public Map()
        {

        }

        public void Generate(int[,] map, int size)
        {
            for (int a = 0; a < map.GetLength(1); a++)
            {
                for (int b = 0; b < map.GetLength(0); b++)
                {
                    int number = map[b, a];

                    if (number>0)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(a * size, b * size, size, size)));
                    }

                    width = (a + 1) * size;
                    height = (b + 1) * size;
                }
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            foreach (CollisionTiles item in collisionTiles)
            {
                item.Draw(sprite);
            }
        }

        
        

    }
}
