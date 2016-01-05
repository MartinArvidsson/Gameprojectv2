using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Gameproject.View
{
    class Statusbar
    {
        private Vector2 livespos, tilespos, timerpos;
        private string lives, tiles, timer;
        private float currenttime;
        private bool timeset = false;
        private bool playertimedout = false;
        private SpriteFont font;
        private SpriteBatch spritebatch;
        private Camera camera;

        public Statusbar(SpriteFont _font,SpriteBatch _spritebatch,Camera _camera)
        {
            font = _font;
            spritebatch = _spritebatch;
            camera = _camera;
        }

        public void Drawplayerlives(int playerlives)
        {
            lives = "Damage taken : " + playerlives.ToString() + " / 3";
            livespos = new Vector2(0.02f, 0.96f);
            livespos = camera.Converttovisualcoords(livespos);
            spritebatch.DrawString(font, lives, livespos, Color.White);
        }
        public void Drawtilescompleted(int playertiles)
        {
            tiles = "Tiles completed: " + playertiles.ToString() + " / 85";
            tilespos = new Vector2(0.02f, 0.92f);
            tilespos = camera.Converttovisualcoords(tilespos);
            spritebatch.DrawString(font, tiles, tilespos, Color.White);
            
        }
        public void Drawtimer(float timeelapsed,float maximumtime)
        {
            if(timeset == false)
            {
                currenttime = maximumtime;
                timeset = true;
            }
            currenttime -= timeelapsed;
            
            timer = "Time remaining:" + (int)currenttime;
            timerpos = new Vector2(0.02f, 0.88f);
            timerpos = camera.Converttovisualcoords(timerpos);
            spritebatch.DrawString(font, timer, timerpos, Color.White);

            if(currenttime <= 0)
            {
                playertimedout = true;
            }
        }

        public bool Returntime()
        {
            return playertimedout;
        }
    }
}
