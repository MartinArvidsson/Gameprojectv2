using Gameproject.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.View
{
    class DrawPlayer
    {
        public void drawplayer(Player player, Camera camera, Texture2D playertexture, SpriteBatch spritebatch, Vector2 playercenter)
        {
            spritebatch.Begin();

            Vector2 currentplayerpos = player.getplayerpos;

            float scale = camera.Scale(player.getplayerradius * 2, playertexture.Width);
            var playervisualpos = camera.Converttovisualcoords(currentplayerpos);

            spritebatch.Draw(playertexture, playervisualpos, null, Color.White, 0, playercenter, scale, SpriteEffects.None, 1f);
            spritebatch.End();
        }
    }
}
