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
        private int playerhit;

        int numberofballs;
        public BallSimulation(int ballstoadd)
        {
            numberofballs = ballstoadd;

            for (int i = 0; i < numberofballs; i++)
            {
                balls.Add(ball = new Ball(rand)); //new ball object
            }
        }

        public void UpdateBall(float Elapsedtime,List<Vector4> playercollisons,List<Vector4> newplayercollisions)
        {
            foreach (Ball ball in balls)
            {
                ball.updatecurrentpos(Elapsedtime);
                hitwall(ball, playercollisons, newplayercollisions);
            }
        }
        public void hitwall(Ball ball, List<Vector4> playercollisons, List<Vector4> newplayercollisions)
        {

            //Balls goes between 0.0 and 1.1
            foreach (Vector4 vector in playercollisons)
            {
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
            }

            foreach (Vector4 vector in newplayercollisions.Skip(1)) //Ignores first pos. since i don't want it to register as a hit when im standing and waiting along the edge.
            {
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
                    playerhit += 1;
                }

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

        public int playerbeenhit
        {
            get
            {
                return playerhit;
            }
        }
    }
}
