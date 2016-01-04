using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gameproject.Model
{
    interface CollisionObserver
    {
        void hitregularwall();

        void hitplayerwall();

        void playerdamaged();
    }
}
