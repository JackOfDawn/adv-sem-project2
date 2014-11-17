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

        public override int GetSpeed()
        {
            return basePistol.GetSpeed() + 1;
        }
        public override int GetDamage()
        {
            return basePistol.GetDamage() + 1;
        }
    }
}
