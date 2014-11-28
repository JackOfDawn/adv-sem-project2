using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;
using Project2_All_Hell_Breaks_Loose.Game.Managers;
namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public interface Weapon
    {
        void Shoot(Vector2 pos, Vector2 direction, BulletManager bulletManagerRef);
        void AddAmmo(int amt);
        bool HasAmmo();
        void UseAmmo();
        int GetSpeed();
        int GetDamage();
        int getAmmo();

        string ClassName();
    }
}
