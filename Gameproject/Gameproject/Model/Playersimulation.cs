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

        public void UpdatePlayer(KeyboardState key,List<Vector4> collisons)
        {
            player.updatecurrentpos(key);
            hitwall(player, collisons);
        }
        public void hitwall(Player player,List<Vector4> collisions)
        {
            foreach (Vector4 vector in collisions)
            {
                int side;

                if (SphereIntersectRectangle(player.getplayerpos, player.getplayerradius, vector, out side))
                {
                    Vector2 newtile = new Vector2(vector.X, vector.Y);

                    if (!playercreatedtiles.Contains(newtile))
                    {
                        playercreatedtiles.Add(newtile);
                    }
                    //Om spelaren krockar med en ruta, skicka kollisionsobjetets kordinater, (Vector4) till en return, som kontrollern använder för att skicka in i update map
                    //Där den nya map:en ritas ut med positionen som spelaren var på som ljusblå ruta, spara tidigare rutan för annars rensas listan efter en loop och bara
                    //Rutan spelaren står på blir ljusblå. alla rutor som är besökta ska vara ljusblåa.
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
