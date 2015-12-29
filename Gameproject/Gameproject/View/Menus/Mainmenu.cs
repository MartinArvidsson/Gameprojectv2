using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.View
{
    class Mainmenu
    {
        Camera camera;
        Vector2 buttonpos,mousepos,buttoncenter,buttonvisualpos;
        Texture2D playbutton;
        float buttonradius = 0.1f;
        float scaledbutton;
        float scaledheight;
        Rectangle rect;
        Rectangle mouserect;
        MouseState currentmouse, previousmouse;

        public Mainmenu(Camera _camera)
        {
            camera = _camera;
            mousepos = Vector2.Zero;
            buttonpos = new Vector2(0.45f, 0.75f);
        }

        public bool Update(Vector2 _mousepos,Texture2D _button,MouseState prevmouse,MouseState currmouse)
        {
            playbutton = _button;
            scaledbutton = camera.Scale(buttonradius *2, playbutton.Width);
            buttoncenter = new Vector2(playbutton.Width / 2, playbutton.Height / 2);
            previousmouse = prevmouse;
            currentmouse = currmouse;

            scaledheight = camera.scaledgame();

            buttonvisualpos = camera.Converttovisualcoords(buttonpos);

            mousepos = _mousepos;

            rect = new Rectangle((int)buttonvisualpos.X, (int)buttonvisualpos.Y, (int)playbutton.Height, (int)playbutton.Width);

            mouserect = new Rectangle((int)mousepos.X, (int)mousepos.Y, 1, 1);
            if(mouserect.Intersects(rect))
            {
                if (previousmouse.LeftButton == ButtonState.Released && currentmouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch _spritebatch,Texture2D _menubackground)
        {
            _spritebatch.Begin();
            _spritebatch.Draw(_menubackground, Vector2.Zero, Color.White);
            _spritebatch.Draw(playbutton, rect, Color.White);
            _spritebatch.End();
        }
    }
}
