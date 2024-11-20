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
            get => width; 
        }
        public int Height 
        { 
            get => height; 
        }

        public Map()
        {

        }

        public void Generate(int[,] map, int size)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[i, y];

                    if (number>0)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(i * size, y * size, size, size)));
                    }

                    width = (i + 1) * size;
                    height = (y + 1) * size;
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
