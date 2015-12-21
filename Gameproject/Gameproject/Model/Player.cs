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
        public Player()
        {
            Playerpos = new Vector2(0.55f, 0.95f);
            velocity = Vector2.Zero;
            movementspeed = new Vector2(0.01f, 0.01f);
        }

        public void updatecurrentpos(KeyboardState key)
        {
            velocity = Vector2.Zero;
            if (key.IsKeyDown(Keys.Up) && !key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Down))
            {
                if (isingamearea() == true)
                {
                    velocity.Y = -movementspeed.Y;
                }
                else
                {
                    velocity.Y = (movementspeed.Y);
                }
            }
            if (key.IsKeyDown(Keys.Down) && !key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Up))
            {
                if (isingamearea() == true)
                {
                    velocity.Y = movementspeed.Y;
                }
                else
                {
                    velocity.Y = -movementspeed.Y;
                }
            }
            if (key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Up) && !key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Down))
            {
                if (isingamearea() == true)
                {
                    velocity.X = movementspeed.X;
                }
                else
                {
                    velocity.X = -movementspeed.X;
                }
            }
            if (key.IsKeyDown(Keys.Left) && !key.IsKeyDown(Keys.Right) && !key.IsKeyDown(Keys.Up) && !key.IsKeyDown(Keys.Down))
            {
                if (isingamearea() == true)
                {
                    velocity.X = -movementspeed.X;
                }
                else
                {
                    velocity.X = movementspeed.X;
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

        public bool isingamearea()
        {
            if (Playerpos.X + Playerradius > 1.0 || Playerpos.X - Playerradius < 0.0) //If player is about to exit out of the gamearea
            {
                return false;
            }
            if (Playerpos.Y + Playerradius > 1.0 || Playerpos.Y - Playerradius < 0.0) //If player is about to exit out of the gamearea
            {
                return false;
            }

            return true;
        }
    }
}
