using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.View
{
    class Drawmap
    {
        public void Drawlevel(int [,] map,List<Texture2D> maptextures,SpriteBatch spritebatch, Camera camera)
        {
            float tilesize = camera.scaledgame();
            float scalegame = camera.Scale(2f, tilesize);
            tilesize *= scalegame;

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int texture = map[y, x];
                    if(texture == -1)
                    continue;
                    Texture2D _texture = maptextures[texture];
                    spritebatch.Draw(_texture, new Rectangle(x * (int)tilesize, (int)tilesize, (int)tilesize, (int)tilesize), Color.White);
                }
            }
            

        }
    }
}
