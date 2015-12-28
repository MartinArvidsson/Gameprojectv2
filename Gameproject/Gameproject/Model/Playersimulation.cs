using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.Model
{
    class Playersimulation
    {
        Player player = new Player();
        Random rand = new Random();
        private List<Vector2> playercreatedtiles = new List<Vector2>();

        public void UpdatePlayer(KeyboardState key, List<Vector4> playercollisons, List<Vector4> ballcollision)
        {
            player.updatecurrentpos(key);
            hitwall(player, playercollisons, ballcollision);
        }
        public void hitwall(Player player, List<Vector4> playercollisons, List<Vector4> ballcollision)
        {
                //Tiles  in ballarea
                foreach (Vector4 vector in ballcollision)
                {
                    int side;

                    if (SphereIntersectRectangle(player.getplayerpos, player.getplayerradius, vector, out side))
                    {
                        Vector2 newtile = new Vector2(vector.X, vector.Y);

                        if (!playercreatedtiles.Contains(newtile))
                        {
                            playercreatedtiles.Add(newtile);
                        }
                    }
                }
                //Outer ring of tiles/ Playerarea
                foreach (Vector4 vector in playercollisons)
                {
                    int side;

                    if (SphereIntersectRectangle(player.getplayerpos, player.getplayerradius, vector, out side))
                    {
                        Vector2 newtile = new Vector2(vector.X, vector.Y);

                        if (!playercreatedtiles.Contains(newtile))
                        {
                            playercreatedtiles.Add(newtile);
                        }
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

        public Player getplayer()
        {
            return player;
        }

        public List<Vector2> getplayercreatedtiles()
        {
            return playercreatedtiles;
        }
    }
}
