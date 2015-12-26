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
        private SpriteBatch spritebatch;
        private Rectangle rect;
        private List<Rectangle> Playertiles = new List<Rectangle>();
        private List<Rectangle> Balltiles = new List<Rectangle>();
        private List<Rectangle> Playercreatingtiles = new List<Rectangle>();
        private List<Texture2D> Maptextures = new List<Texture2D>();
        private List<Vector2> Tilesbyplayer = new List<Vector2>();

        private int previoustiles;
        private float tilesize;
        private float scalegame;

        public void Drawlevel(int [,] map,List<Texture2D> maptextures,SpriteBatch _spritebatch, Camera camera)
        {
            spritebatch = _spritebatch;
            Maptextures = maptextures;
            tilesize = camera.scaledgame();
            scalegame = camera.Scale(0.1f, tilesize);
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

                    Texture2D _texture = Maptextures[texture];
                    rect =  new Rectangle(x * (int)tilesize, y * (int)tilesize, (int)tilesize, (int)tilesize);
                    
                        if (_texture == Maptextures[0])
                        {
                            if (!Balltiles.Contains(rect))
                            {
                                Balltiles.Add(rect);
                            }
                        }

                        if (_texture == Maptextures[1])
                        {
                            if (!Playertiles.Contains(rect))
                            {
                                Playertiles.Add(rect);
                            }
                        }
                    spritebatch.Draw(_texture,rect, Color.White);
                }
            }
            spritebatch.End();
        }

        public void Updatelevel(List<Vector2> _Tilesbyplayer)
        {
            spritebatch.Begin();
            foreach (Vector2 newtile in _Tilesbyplayer)
            {
                Texture2D _texture = Maptextures[2];
                rect = new Rectangle((int)newtile.X, (int)newtile.Y, (int)tilesize, (int)tilesize);

                if (!Playercreatingtiles.Contains(rect))
                {
                    Playercreatingtiles.Add(rect);
                }
                if(Playertiles.Contains(rect))
                {
                    Playertiles.Remove(rect);
                }
                if(Balltiles.Contains(rect))
                {
                    Balltiles.Remove(rect);
                }
                spritebatch.Draw(_texture, rect, Color.White);
            }

            if (Playertiles.Contains(Playercreatingtiles.FirstOrDefault()) && Playertiles.Contains(Playercreatingtiles.LastOrDefault())
                    && Playercreatingtiles.FirstOrDefault() != Playercreatingtiles.LastOrDefault())
            {
                //foreach (Vector2 newtile in _Tilesbyplayer)
                //{
                //    Texture2D _texture = Maptextures[1];
                //    rect = new Rectangle((int)newtile.X, (int)newtile.Y, (int)tilesize, (int)tilesize);

                //    if (Playercreatingtiles.Contains(rect))
                //    {
                //        Playercreatingtiles.Remove(rect); 
                //    }
                //    if(!Playertiles.Contains(rect))
                //    {
                //        Playertiles.Add(rect);
                //    }
                //    spritebatch.Draw(_texture, rect, Color.White);
                //}
            }
            spritebatch.End();
        }

        public void updatedtilestoadd(List<Vector2> newtiles)
        {
            if(previoustiles != newtiles.Count)
            {
                previoustiles = newtiles.Count;
                Tilesbyplayer = newtiles;
            }
        }

        public List<Vector2> Returnplayertilestoadd()
        {
            return Tilesbyplayer;
        }

        public List<Rectangle> Returnballcollisions()
        {
            return Playertiles;
        }


        public List<Rectangle> Returnplayercollisions()
        {
            return Balltiles;
        }

        public List<Rectangle> Returnplayercreatedtiles()
        {
            return Playercreatingtiles;
        }
    }
}
