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
        
        int numberofballs = 5;
        public BallSimulation()
        {
            for (int i = 0; i < numberofballs; i++)
            {
                balls.Add(ball = new Ball(rand)); //new ball object
            }
        }

        public void UpdateBall(float Elapsedtime,List<Vector4> collisons)
        {
            foreach (Ball ball in balls)
            {
                ball.updatecurrentpos(Elapsedtime);
                hitwall(ball, collisons);
            }
        }
        public void hitwall(Ball ball,List<Vector4> collisions)
        {
            //Console.WriteLine(ball.BallPos);
            //Balls goes between 0.0 and 1.1
            foreach (Vector4 vector in collisions)
            {
                //Console.WriteLine(vector);

                int side;

                if (SphereIntersectRectangle(ball.getballpos, ball.getballradius, vector, out side))
                {
                    switch (side)
                    {
                        case 1:
                            ball.setballVelocityX(1);
                            break;
                        case -1:
                            ball.setballVelocityX(-1);
                            break;
                        case 2:
                            ball.setballVelocityY(1);
                            break;
                        case -2:
                            ball.setballVelocityY(-1);
                            break;
                    }
                }

                /*
                if ((ball.BallPos.Y + ball.getballradius) > vector.Y || (ball.BallPos.Y - ball.getballradius) < 1) //If ball bounces ^ v
                {
                    ball.setballVelocityY();
                }   
                 * */
            }
        }

        private bool SphereIntersectRectangle(Vector2 pos, float radius, Vector4 rect, out int side)
        {
            side = 0;

            Vector2 centerOfRect = new Vector2(rect.X + rect.Z / 2f, rect.Y + rect.W / 2f);

            Vector2 v = new Vector2(MathHelper.Clamp(pos.X, rect.X, rect.X + rect.Z),
                                MathHelper.Clamp(pos.Y, rect.Y, rect.Y + rect.W));

            Vector2 direction = pos - v;
            float distanceSquared = direction.LengthSquared();

            Vector2 centerToCenter = (pos - centerOfRect);
            centerToCenter.Normalize();

            if (Math.Abs(centerToCenter.X) > Math.Abs(centerToCenter.Y))
            {
                side = centerToCenter.X > 0 ? 1 : -1;
            }
            else
            {
                side = centerToCenter.Y > 0 ? 2 : -2;
            }

            return ((distanceSquared > 0) && (distanceSquared < radius * radius));
        }

        public List<Ball> getballs()
        {
            return balls;
        }
    }
}
