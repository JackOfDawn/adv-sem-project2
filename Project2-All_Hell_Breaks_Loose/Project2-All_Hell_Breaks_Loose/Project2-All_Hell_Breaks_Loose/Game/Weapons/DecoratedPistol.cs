using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public class DecoratedPistol : AbstractPistol
    {
        private AbstractPistol basePistol;

        public DecoratedPistol(AbstractPistol pistol)
        {
            basePistol = pistol;
        }

        public override int getSpeed()
        {
            return basePistol.getSpeed() + 1;
        }
        public override int getDamage()
        {
            return basePistol.getDamage() + 1;
        }
    }
}
