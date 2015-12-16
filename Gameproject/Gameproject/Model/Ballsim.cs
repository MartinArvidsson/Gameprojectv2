using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.Model
{
    class BallSimulation
    {
        private List<Ball> balls = new List<Ball>();
        public Ball ball;
        Random rand = new Random();
        int numberofballs = 4;
        public BallSimulation()
        {
            for (int i = 0; i < numberofballs; i++)
            {
                balls.Add(ball = new Ball(rand)); //new ball object
            }
        }

        public void UpdateBall(float Elapsedtime)
        {
            foreach (Ball ball in balls)
            {
                hitwall(ball);
                ball.updatecurrentpos(Elapsedtime);
            }
        }
        public void hitwall(Ball ball)
        {

            if ((ball.BallPos.X + ball.getballradius) > 1 || (ball.BallPos.X - ball.getballradius) < 0) //If ball bounces <---->
            {
                ball.setballVelocityX();
            }
            if ((ball.BallPos.Y + ball.getballradius) > 1 || (ball.BallPos.Y - ball.getballradius) < 0) //If ball bounces ^ v
            {
                ball.setballVelocityY();
            }

        }

        public List<Ball> getballs()
        {
            return balls;
        }
    }
}
