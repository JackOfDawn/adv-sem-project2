using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public class ShotGun : AbstractShotGun
    {
        private const int SPEED = 3;
        private const int DAMAGE = 5;

        public ShotGun()
        {
            ammo = 10;
        }
        public override int GetSpeed()
        {
            return SPEED;
        }
        public override int GetDamage()
        {
            return DAMAGE;
        }
    }
}
