using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.Controller
{
    class LevelThree
    {
        public int[,] getmap()
        {
            int[,] map;

            return map = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,1},
                {1,0,0,1,0,0,0,1,0,1},
                {1,0,1,0,1,0,0,0,0,1},
                {1,0,0,0,0,0,0,1,0,1},
                {1,0,1,0,0,0,0,0,0,1},
                {1,0,0,0,0,1,0,1,0,1},
                {1,0,1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,1,0,1},
                {1,1,1,1,1,1,1,1,1,1}
            };
        }
    }
}
