using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gameproject.View;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Gameproject.Controller
{
    class Menucontroller
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spritebatch;
        ContentManager content;
        Camera camera;
        Texture2D menubackground, playbutton;
        Mainmenu mainmenu;
        MouseState currentmouse, previousmouse;

        bool hasplayerclicked = false;
        bool hasplayerclickedplay = false;

        public Menucontroller(GraphicsDeviceManager _graphics)
        {
            graphics = _graphics;
        }

        public void LoadContent(SpriteBatch _spritebatch,ContentManager _content,Camera _camera)
        {
            spritebatch = _spritebatch;
            content = _content;
            camera = _camera;

            menubackground = content.Load<Texture2D>("Menubackground.png");
            playbutton = content.Load<Texture2D>("playbutton.png");
            mainmenu = new Mainmenu(camera);
        }

        public bool Update()
        {
            previousmouse = currentmouse;
            currentmouse = Mouse.GetState();

            var mousepos = new Vector2(currentmouse.X, currentmouse.Y);
            hasplayerclickedplay = mainmenu.Update(mousepos, playbutton, hasplayerclicked);

            if(previousmouse.LeftButton == ButtonState.Released && currentmouse.LeftButton == ButtonState.Pressed)
            {
                hasplayerclicked = true;
                hasplayerclickedplay = mainmenu.Update(mousepos, playbutton, hasplayerclicked);
            }
            return hasplayerclickedplay;
        }

        public void Draw()
        {
            mainmenu.Draw(spritebatch,menubackground);
        }
        
    }
}
