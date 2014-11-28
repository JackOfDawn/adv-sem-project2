using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public class DecoratedShotgun : AbstractShotGun
    {
        private AbstractShotGun baseShotgun;

        public DecoratedShotgun(AbstractShotGun Shotgun)
        {
            baseShotgun = Shotgun;
            base.AddAmmo(Shotgun.getAmmo());
        }

        public override int GetSpeed()
        {
            return baseShotgun.GetSpeed();
        }
        public override int GetDamage()
        {
            return baseShotgun.GetDamage() + 3;
        }
    }
}
