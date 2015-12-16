using Gameproject.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.View
{
    class Drawballs
    {
        public void drawallballs(List<Ball> totalballs,Camera camera,Texture2D balltexture,SpriteBatch spritebatch,Vector2 ballcenter)
        {
            foreach (Ball ball in totalballs)
            {
                Vector2 currentballpos = ball.getballpos;

                float scale = camera.Scale(ball.getballradius * 2, balltexture.Width);

                var ballvisualpos = camera.Converttovisualcoords(currentballpos, scale);

                spritebatch.Draw(balltexture, ballvisualpos, null, Color.White, 0, ballcenter, scale, SpriteEffects.None, 1f);
            }
        }
    }
}
