using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gameproject.View
{
    class Camera
    {
        private int fieldsize;
        private int sizeOfTile = 32;
        private int width;
        private int height;
        private float scale;
        private float gamescaling;

        public void SetFieldSize(Viewport board) //If width is bigger than height use it for scaling, otherwhise the other way around
        {
            width = board.Width;
            height = board.Height;

            if (width < height)
            {
                fieldsize = width;
            }
            else
            {
                fieldsize = height;
            }
        }
        //, float scale
        public Vector2 Converttovisualcoords(Vector2 coords) //gamecoords to visualcoords
        {
            float visualX = (coords.X * fieldsize);
            float visualY = (coords.Y * fieldsize);

            return new Vector2(visualX, visualY);
        }

        public Vector2 convertologicalcoords(Vector2 coords)
        {
            float logicX = (coords.X) / fieldsize;
            float logicY = (coords.Y) / fieldsize;

            return new Vector2(logicX, logicY);
        }

        public float Scale(float size, float width) //scales the particle
        {
            return scale = fieldsize * size / width;

        }

        public int ReturnFieldsize()
        {
            return fieldsize;
        }

        public void scalegame(GraphicsDeviceManager graphics)
        {

            float XScale = (float)graphics.GraphicsDevice.Viewport.Width / (sizeOfTile * 10);
            float YScale = (float)graphics.GraphicsDevice.Viewport.Height / (sizeOfTile * 10);

            if (XScale < YScale)
            {
                gamescaling = XScale;
            }
            else
            {
                gamescaling = YScale;
            }
        }

        public float scaledgame()
        {
            return gamescaling;
        }
    }
}
