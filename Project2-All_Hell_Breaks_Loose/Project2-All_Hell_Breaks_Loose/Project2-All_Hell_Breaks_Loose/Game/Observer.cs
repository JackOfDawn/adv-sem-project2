using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public interface Observer
    {
        void notify(int money = 0, int ammo = 0);
    }
}
