using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public class Pistol : AbstractPistol
    {
        private const int SPEED = 5;
        private const int DAMAGE = 4;

        public Pistol()
        {

        }
        public override int getSpeed()
        {
            return SPEED;
        }
        public override int getDamage()
        {
            return DAMAGE;
        }

    }
}
