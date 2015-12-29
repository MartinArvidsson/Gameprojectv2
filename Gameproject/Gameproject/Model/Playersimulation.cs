using Gameproject.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.Model
{
    class Playersimulation
    {
        Player player = new Player();
        Random rand = new Random();
        private List<Rectangle> playercreatedtiles = new List<Rectangle>();
        private List<Rectangle> playercollisions = new List<Rectangle>();
        private List<Rectangle> ballcollisions = new List<Rectangle>();
        private bool clearlist;
        private int playerhit;
        public void SetBool(bool finishedupdating)
        {
            clearlist = finishedupdating;
        }

        public void setInt(int _playerhit)
        {
            playerhit = _playerhit;
            player.updateplayerlifes(playerhit);
        }

        public void setcollisions(List<Rectangle> _playercollisons, List<Rectangle> _ballcollisions)
        {
            playercollisions = _playercollisons;
            ballcollisions = _ballcollisions;
        }

        public void UpdatePlayer(KeyboardState key)
        {
            player.updatecurrentpos(key);
        }
        public void hitwall(Camera camera)
        {
            if(clearlist == true)
            {
                playercreatedtiles.Clear();
            }

            //Outer ring of tiles/ Playerarea
            foreach (Rectangle rect in playercollisions)
            {
                if (rect.Contains(camera.Converttovisualcoords(player.getplayerpos)))
                {
                    if (!playercreatedtiles.Contains(rect))
                    {
                        playercreatedtiles.Add(rect);
                    }
                }
            }

            //Tiles  in ballarea
            foreach (Rectangle rect in ballcollisions)
            {
                if (rect.Contains(camera.Converttovisualcoords(player.getplayerpos)))
                {
                    if (!playercreatedtiles.Contains(rect))
                    {
                        playercreatedtiles.Add(rect);
                    }
                }
            }
        }

        public Player getplayer()
        {
            return player;
        }

        public List<Rectangle> getplayercreatedtiles()
        {
            return playercreatedtiles;

        }

        public bool isgameover
        {
            get
            {
                return player.isgameover;
            }
        }
    }
}
