using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.Managers;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public abstract class AbstractPistol : Weapon
    {
        public void Shoot(Vector2 pos, Vector2 direction, BulletManager bulletManagerRef)
        {
            Bullet newBullet = new Bullet(pos, GetSpeed(), direction, GetDamage());
            bulletManagerRef.AddBullet(newBullet);
        }

        public  void AddAmmo(int amt)
        {
            //pistol doesn't use ammo
        }
        public bool HasAmmo()
        {
            return true;
        }
        public void UseAmmo()
        {
             //pistol doesn't use ammo
        }
        public abstract int GetSpeed();
        public abstract int GetDamage();
    }
}
