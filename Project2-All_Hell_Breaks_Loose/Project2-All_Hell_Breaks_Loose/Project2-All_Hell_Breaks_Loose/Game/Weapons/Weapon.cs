using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public interface Weapon
    {
        void shoot(Vector2 pos, Vector2 direction, BulletManager bulletManagerRef);
        void addAmmo(int amt);
        bool hasAmmo();
        void useAmmo();
        int getSpeed();
        int getDamage();
    }
}
