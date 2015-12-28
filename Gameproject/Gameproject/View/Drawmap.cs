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
        private Rectangle previouslasttile;

        private int previoustiles;
        private float tilesize;
        private float scalegame;

        private bool finishedcreating = false;

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

        public void updatedtilestoadd(List<Vector2> newtiles)
        {
            if (previoustiles != newtiles.Count)
            {
                previoustiles = newtiles.Count;
                Tilesbyplayer = newtiles;
            }
        }

        public List<Vector2> Returnplayertilestoadd()
        {
            return Tilesbyplayer;
        }

        public void Updatelevel(List<Vector2> _Tilesbyplayer)
        {
            //Console.WriteLine(_Tilesbyplayer.Count);
            if (_Tilesbyplayer.Count == 0 && finishedcreating == true)
            {
                //finishedcreating = false;
            }
            spritebatch.Begin();
            foreach (Vector2 newtile in _Tilesbyplayer)
            {
                Texture2D _texture = Maptextures[2];
                rect = new Rectangle((int)newtile.X, (int)newtile.Y, (int)tilesize, (int)tilesize);
                if (!Playercreatingtiles.Contains(rect))
                {
                    Playercreatingtiles.Add(rect); //Ljusblåa tiles.
                }
                if (Balltiles.Contains(rect))
                {
                    Balltiles.Remove(rect); //Gråa tiles.
                }
                spritebatch.Draw(_texture, rect, Color.White);
            }

            if(Playercreatingtiles.Count > 0)
            {
                previouslasttile = Playercreatingtiles.Last();
            }

            spritebatch.End();
            if(Playercreatingtiles.Count > 0)
            {
                //BUGGAR DÄRFÖR ATT DEN KOLLAR FÖNSTRET DEN GÅR IN I SEN DEN SOM DEN LÄMNAR, DÄRFÖR BLIR DEN ALLTID BARA TVÅ STOR
                //Eller inte...
                if (Playertiles.Contains(Playercreatingtiles.First()) && Playertiles.Contains(Playercreatingtiles.Last())
                && Playercreatingtiles.First() != Playercreatingtiles.Last())
                {
                    //Problem är att eftersom Playercreatingtiles rensas, så rensas också rutorna som ska få mörkblå färg.
                    //Rektangeln är kvar men spriten försvinner..
                    FinishedUpdating(Playercreatingtiles);
                    Playercreatingtiles.Clear();
                    finishedcreating = true;
                }
            }
        }

        public void FinishedUpdating(List<Rectangle> _playercreatedtiles)
        {
            Texture2D _texture = Maptextures[1];
            foreach (Rectangle _newtile in _playercreatedtiles)
            {                
                if (!Playertiles.Contains(_newtile))
                {
                    Playertiles.Add(_newtile); //Mörkblåa tiles.
                }
            }

            spritebatch.Begin();
            foreach(Rectangle _rect in Playertiles)
            {
                spritebatch.Draw(_texture, _rect, Color.White);
            }
            spritebatch.End();
        }

        public bool Returnfinishedcreating()
        {
            return finishedcreating;
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
