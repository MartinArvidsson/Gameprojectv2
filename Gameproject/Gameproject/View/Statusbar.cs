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
           
            livespos = new Vector2(0.02f, 0.96f);
            livespos = camera.Converttovisualcoords(livespos);
            if (playerlives < 2)
            {
                lives = "Damage taken : " + playerlives.ToString() + " / 3";
                spritebatch.DrawString(font, lives, new Vector2(livespos.X + 1, livespos.Y), Color.Black);
                spritebatch.DrawString(font, lives, new Vector2(livespos.X - 1, livespos.Y), Color.Black);
                spritebatch.DrawString(font, lives, new Vector2(livespos.X, livespos.Y + 1), Color.Black);
                spritebatch.DrawString(font, lives, new Vector2(livespos.X, livespos.Y - 1), Color.Black);
                spritebatch.DrawString(font, lives, livespos, Color.White);
            }
            else
            {
                lives = "Damage taken : " + playerlives.ToString() + " / 3 One more hit and you are out!";
                spritebatch.DrawString(font, lives, new Vector2(livespos.X + 1, livespos.Y), Color.Black);
                spritebatch.DrawString(font, lives, new Vector2(livespos.X - 1, livespos.Y), Color.Black);
                spritebatch.DrawString(font, lives, new Vector2(livespos.X, livespos.Y + 1), Color.Black);
                spritebatch.DrawString(font, lives, new Vector2(livespos.X, livespos.Y - 1), Color.Black);
                spritebatch.DrawString(font, lives, livespos, Color.Red);
            }
        }
        public void Drawtilescompleted(int playertiles)
        {
            tiles = "Tiles completed: " + playertiles.ToString() + " / 85";
            tilespos = new Vector2(0.02f, 0.92f);
            tilespos = camera.Converttovisualcoords(tilespos);
            spritebatch.DrawString(font, tiles, new Vector2(tilespos.X + 1, tilespos.Y), Color.Black);
            spritebatch.DrawString(font, tiles, new Vector2(tilespos.X - 1, tilespos.Y), Color.Black);
            spritebatch.DrawString(font, tiles, new Vector2(tilespos.X, tilespos.Y + 1), Color.Black);
            spritebatch.DrawString(font, tiles, new Vector2(tilespos.X, tilespos.Y - 1), Color.Black);
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
            
           
            timerpos = new Vector2(0.02f, 0.88f);
            timerpos = camera.Converttovisualcoords(timerpos);
            if(currenttime > 20)
            {
                timer = "Time remaining:" + (int)currenttime;
                spritebatch.DrawString(font, timer, new Vector2(timerpos.X + 1, timerpos.Y), Color.Black);
                spritebatch.DrawString(font, timer, new Vector2(timerpos.X - 1, timerpos.Y), Color.Black);
                spritebatch.DrawString(font, timer, new Vector2(timerpos.X, timerpos.Y + 1), Color.Black);
                spritebatch.DrawString(font, timer, new Vector2(timerpos.X, timerpos.Y - 1), Color.Black);
                spritebatch.DrawString(font, timer, timerpos, Color.White);
            }
            else
            {
                timer = "Time remaining:" + (int)currenttime +" Time is running out!";
                spritebatch.DrawString(font, timer, new Vector2(tilespos.X + 1, timerpos.Y), Color.Black);
                spritebatch.DrawString(font, timer, new Vector2(tilespos.X - 1, timerpos.Y), Color.Black);
                spritebatch.DrawString(font, timer, new Vector2(tilespos.X, timerpos.Y + 1), Color.Black);
                spritebatch.DrawString(font, timer, new Vector2(tilespos.X, timerpos.Y - 1), Color.Black);
                spritebatch.DrawString(font, timer, timerpos, Color.Red);
            }
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
