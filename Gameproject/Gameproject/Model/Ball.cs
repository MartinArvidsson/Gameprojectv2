using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.Model
{
    class Ball
    {
        public Vector2 BallPos;
        private float maxspeed = 0.4f;
        private float minspeed = 0.2f;
        private Vector2 BallVelocity;
        private float Ballradius = 0.02f;
        private Vector2 randomdirection;
        private Random rand;
        public Ball(Random _rand)
        {
            BallPos = new Vector2(0.5f, 0.5f); //Where the ball starts
            rand = _rand;
            randomdirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.7f);
            //normalize to get it spherical vector with length 1.0
            randomdirection.Normalize();
            randomdirection = randomdirection * ((float)rand.NextDouble() * maxspeed + minspeed);
            BallVelocity = randomdirection;
        }

        public void updatecurrentpos(float elapsedtime)
        {
            BallPos += BallVelocity * elapsedtime;
        }

        public float getballradius //Gets the radius
        {
            get
            {
                return Ballradius;
            }
        }

        public Vector2 getballpos //Gets the position
        {
            get
            {
                return BallPos;
            }
        }

        //Makes velocity the opposite of what it was since it hit a wall and needs to turn, otherwhise it keeps going in same direction.
        public void setballVelocityX() //Sets a new velocity on X
        {
            BallVelocity.X = -BallVelocity.X;
            float randomY = (float)rand.NextDouble();
            randomY = randomY * ((float)rand.NextDouble());
            if (randomY >= minspeed && randomY <= maxspeed)
            {
                BallVelocity.Y = randomY;
            }
        }

        public void setballVelocityY() //Sets a new Velocity on Y 
        {
            BallVelocity.Y = -BallVelocity.Y;
            float randomX = (float)rand.NextDouble();
            randomX = randomX * ((float)rand.NextDouble());
            if (randomX >= minspeed && randomX <= maxspeed)
            {
                BallVelocity.X = randomX;
            }
        }
    }
}
