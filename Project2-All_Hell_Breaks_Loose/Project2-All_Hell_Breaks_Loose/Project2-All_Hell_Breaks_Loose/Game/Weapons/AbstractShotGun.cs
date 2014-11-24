using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;

using Project2_All_Hell_Breaks_Loose.Game.Managers;

namespace Project2_All_Hell_Breaks_Loose.Game.Weapons
{
    public abstract class AbstractShotGun : Weapon
    {
        private const float ROTATION = (float)Math.PI / 12;


        protected int ammo;

        public void Shoot(Vector2 pos, Vector2 direction, BulletManager bulletManagerRef)
        {
            if(HasAmmo())
            {
                Bullet newBullet = new Bullet(pos, GetSpeed(), direction, GetDamage());
                Bullet newBullet2 = new Bullet(pos, GetSpeed(), rotate(direction, ROTATION), GetDamage());
                Bullet newBullet3 = new Bullet(pos, GetSpeed(), rotate(direction, -ROTATION), GetDamage());
                bulletManagerRef.AddBullet(newBullet);
                bulletManagerRef.AddBullet(newBullet2);
                bulletManagerRef.AddBullet(newBullet3);
                UseAmmo();
            }
            
        }

        public void AddAmmo(int amt)
        {
            ammo += amt;
        }
        public bool HasAmmo()
        {
            return ammo > 0;
        }
        public void UseAmmo()
        {
            ammo--;
        }
        public abstract int GetSpeed();
        public abstract int GetDamage();

        private Vector2 rotate(Vector2 direction, float rotation)
        {
            Vector2 newDirection = new Vector2();
            newDirection.X = direction.X * (float)Math.Cos(rotation) + direction.Y * (float)Math.Sin(rotation);
            newDirection.Y = -direction.X * (float)Math.Sin(rotation) + direction.Y * (float)Math.Cos(rotation);

            return newDirection;
        }
    }
}
