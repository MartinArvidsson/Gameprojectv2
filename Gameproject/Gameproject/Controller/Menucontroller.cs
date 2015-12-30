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
        Texture2D menubackground,playerwonbackground,playerlostbackground, playbutton,restartbutton;
        Mainmenu mainmenu;
        Playerwonmenu playerwonmenu;
        Playerlostmenu playerlostmenu;
        MouseState currentmouse, previousmouse;

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
            playerwonbackground = content.Load<Texture2D>("Playerwonbackground.png");
            playerlostbackground = content.Load<Texture2D>("Playerdiedbackground.png");
            playbutton = content.Load<Texture2D>("playbutton.png");
            restartbutton = content.Load<Texture2D>("restartbutton.png");
            mainmenu = new Mainmenu(camera);
            playerwonmenu = new Playerwonmenu(camera);
            playerlostmenu = new Playerlostmenu(camera);

        }

        public bool Update(int currentcase)
        {
            if(currentcase == 1)
            {
                previousmouse = currentmouse;
                currentmouse = Mouse.GetState();

                var mousepos = new Vector2(currentmouse.X, currentmouse.Y);
                hasplayerclickedplay = mainmenu.Update(mousepos, playbutton, previousmouse, currentmouse);

                return hasplayerclickedplay;
            }
            if (currentcase == 2)
            {
                previousmouse = currentmouse;
                currentmouse = Mouse.GetState();

                var mousepos = new Vector2(currentmouse.X, currentmouse.Y);
                hasplayerclickedplay = playerwonmenu.Update(mousepos, playbutton, previousmouse, currentmouse);

                return hasplayerclickedplay;
            }
            if (currentcase == 3)
            {
                previousmouse = currentmouse;
                currentmouse = Mouse.GetState();

                var mousepos = new Vector2(currentmouse.X, currentmouse.Y);
                hasplayerclickedplay = playerlostmenu.Update(mousepos, restartbutton, previousmouse, currentmouse);

                return hasplayerclickedplay;
            }
            return false;      
        }

        public bool exitgame()
        {
            return mainmenu.exitgame();
        }

        public void Draw(int currentcase)
        {
            if(currentcase == 1)
            {
                mainmenu.Draw(spritebatch, menubackground);
            }
            if(currentcase == 2)
            {
                playerwonmenu.Draw(spritebatch, playerwonbackground);
            }
            if(currentcase == 3)
            {
                playerlostmenu.Draw(spritebatch, playerlostbackground);
            }
        }
        
    }
}
