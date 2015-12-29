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
        private List<Rectangle> Tilesbyplayer = new List<Rectangle>();
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

        public void updatedtilestoadd(List<Rectangle> newtiles)
        {
            if (previoustiles != newtiles.Count)
            {
                previoustiles = newtiles.Count;
                Tilesbyplayer = newtiles;
            }
        }

        public List<Rectangle> Returnplayertilestoadd()
        {
            return Tilesbyplayer;
        }

        public void Updatelevel(List<Rectangle> _Tilesbyplayer)
        {
            if (finishedcreating == true) //Om applikationen har körts och reset:at listan en gång. återställ variabeln till false så att listan
            {                                                          //Inte töms igen förens man har skapat en till bit av "Väggen".
                finishedcreating = false;
            }
            spritebatch.Begin();
            foreach (Rectangle rect in _Tilesbyplayer) //Kolla varje objekt i listan enligt hur man har rört sig. och rita ut ljusblåa tiles enligt vägen.
            {
                Texture2D _texture = Maptextures[2];
                if (!Playercreatingtiles.Contains(rect)) //Om inte objektet redan finns i listan med ljusblåa brickor, lägg till den.
                {
                    Playercreatingtiles.Add(rect); //Ljusblåa tiles.
                }
                if (Balltiles.Contains(rect)) //Om objektet redan finns i "bollarean" ta bort det då det nu mera är en vägg.
                {
                    Balltiles.Remove(rect); //Gråa tiles.
                }
                spritebatch.Draw(_texture, rect, Color.White);
                //Ritar ut den ljusblåa "vägen".
            }

            if(Playercreatingtiles.Count > 0) //Om man har gått > 0 steg, sätt previouslasttile till sista pos. i listan, används för att veta var man ska starta att rita efter att
            {                                 //man har lyckats skapa en bit vägg en gång.
                previouslasttile = Playercreatingtiles.Last();
            }

            if(Playercreatingtiles.Count > 0)
            {
                //BUGGAR DÄRFÖR ATT DEN KOLLAR FÖNSTRET DEN GÅR IN I SEN DEN SOM DEN LÄMNAR, DÄRFÖR BLIR DEN ALLTID BARA TVÅ STOR
                //Eller inte...

                //Problem är att eftersom Playercreatingtiles rensas, så rensas också rutorna som ska få mörkblå färg.
                //Rektangeln är kvar men spriten försvinner..

                if (Playertiles.Contains(Playercreatingtiles.First()) && Playertiles.Contains(Playercreatingtiles.Last())
                && Playercreatingtiles.First() != Playercreatingtiles.Last())
                { //Om man har gått minstonde 2 steg och kraven ovan fylls, skapa mörkblåa brickor av dom ljusblåa.
                    FinishedUpdating(Playercreatingtiles);
                    //Playercreatingtiles.Clear();
                    finishedcreating = true;
                }
            }
            foreach (Rectangle _rect in Playertiles) //Mörkblåa rutor ritas ut.
            {
                Texture2D _texture = Maptextures[1];
                spritebatch.Draw(_texture, _rect, Color.White);
            }
            spritebatch.End();
        }

        public void FinishedUpdating(List<Rectangle> _playercreatedtiles)
        {
            foreach (Rectangle _newtile in _playercreatedtiles)
            {                
                if (!Playertiles.Contains(_newtile))
                {
                    Playertiles.Add(_newtile); //Mörkblåa brickor läggs till.
                }
            }
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
