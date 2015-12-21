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
        private Rectangle rect;
        private List<Rectangle> Playertiles = new List<Rectangle>();
        private List<Rectangle> Balltiles = new List<Rectangle>();
        private List<Rectangle> Playercreatingtiles = new List<Rectangle>();

        private bool runonce = true;

        public void Drawlevel(int [,] map,List<Texture2D> maptextures,SpriteBatch spritebatch, Camera camera)
        {
            float tilesize = camera.scaledgame();
            float scalegame = camera.Scale(0.1f, tilesize);
            tilesize *= scalegame;
            spritebatch.Begin();
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int texture = map[y, x];
                    if (texture == -1)
                    {
                        continue;
                    }
                    Texture2D _texture = maptextures[texture];
                    rect =  new Rectangle(x * (int)tilesize, y * (int)tilesize, (int)tilesize, (int)tilesize);
                    
                    if (runonce == true)
                    {
                        if (_texture == maptextures[1])
                        {
                            Playertiles.Add(rect);
                        }

                        if(_texture == maptextures[0])
                        {
                            Balltiles.Add(rect);
                        }

                        else
                        {
                            Playercreatingtiles.Add(rect);
                        }
                    }
                    spritebatch.Draw(_texture,rect, Color.White);
                }
            }
            runonce = false;
            spritebatch.End();
        }

        public List<Rectangle> Returnballcollisions()
        {
            return Playertiles;
        }


        public List<Rectangle> Returnplayercollisions()
        {
            return Balltiles;
        }

        public List<Rectangle> Returncreatingtiles()
        {
            return Playercreatingtiles;
        }

    }
}
