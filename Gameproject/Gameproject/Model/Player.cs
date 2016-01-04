using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.Model
{
    class Player
    {
        public Vector2 Playerpos,velocity,movementspeed;
        private float Playerradius = 0.03f;
        private int playerlife = 3;
        private bool gameover = false;
        public Player()
        {
            Playerpos = new Vector2(0.55f, 0.95f);
            velocity = Vector2.Zero;
            movementspeed = new Vector2(0.008f, 0.008f);
        }

        public void updateplayerlifes(int playerhits)
        {
            if(playerhits >= playerlife)
            {
                gameover = true;
            }
        }
        public void updatecurrentpos(KeyboardState key)
        {
            velocity = Vector2.Zero;
            if (key.IsKeyDown(Keys.Up) && !key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Down)) //Up
            {
                if (Playerpos.Y - Playerradius < 0)
                {
                    velocity.Y = movementspeed.Y;
                }
                else
                {
                    velocity.Y = -movementspeed.Y;
                }
            }
            if (key.IsKeyDown(Keys.Down) && !key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Up)) //Down
            {
                if (Playerpos.Y + Playerradius > 1)
                {
                    velocity.Y = -movementspeed.Y; 
                }
                else
                {
                    velocity.Y = movementspeed.Y;
                }
            }
            if (key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Up) && !key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Down)) //Right
            {
                if (Playerpos.X + Playerradius > 1)
                {
                    velocity.X = -movementspeed.X;
                }
                else
                {
                    velocity.X = movementspeed.X;
                }
            }
            if (key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Up) && !key.IsKeyDown(Keys.Down)) //left
            {
                if (Playerpos.X - Playerradius < 0)
                {
                    velocity.X = movementspeed.X;
                }
                else
                {
                    velocity.X = -movementspeed.X;
                }
            }

            Playerpos += velocity;
        }

        public float getplayerradius //Gets the radius
        {
            get
            {
                return Playerradius;
            }
        }

        public Vector2 getplayerpos //Gets the position
        {
            get
            {
                return Playerpos;
            }
        }

        public bool isgameover
        {
            get
            {
                return gameover;
            }
        }
    }
}
