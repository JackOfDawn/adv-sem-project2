using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public abstract class AbstractPistol : Weapon
    {
        public void shoot(Vector2 pos, Vector2 direction, BulletManager bulletManagerRef)
        {
            Bullet newBullet = new Bullet(pos, getSpeed(), direction, getDamage());
            bulletManagerRef.addBullet(newBullet);
        }

        public  void addAmmo(int amt)
        {
            //pistol doesn't use ammo
        }
        public bool hasAmmo()
        {
            return true;
        }
        public void useAmmo()
        {
             //pistol doesn't use ammo
        }
        public abstract int getSpeed();
        public abstract int getDamage();
    }
}
