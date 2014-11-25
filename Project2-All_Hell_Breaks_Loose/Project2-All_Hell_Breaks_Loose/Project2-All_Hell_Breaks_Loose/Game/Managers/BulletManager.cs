using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;

namespace Project2_All_Hell_Breaks_Loose.Game.Managers
{
    public class BulletManager
    {
        private List<Bullet> bullets;
        private List<Bullet> bulletsToRemove;
        private EnemyManager enemyManager;

        public BulletManager(EnemyManager enemyManagerRef)
        {
            bullets = new List<Bullet>();
            bulletsToRemove = new List<Bullet>();
            enemyManager = enemyManagerRef;
        }

        public void AddBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        public void AddBullets(List<Bullet>bulletsToAdd)
        {
            bullets.AddRange(bulletsToAdd);
        }

        public void DeleteBullet(Bullet bullet)
        {
            bulletsToRemove.Add(bullet);
        }

        public void DeleteBullet(int index)
        {
            bulletsToRemove.Add(bullets[index]);
        }

        private void RemoveAtEndOfFrame()
        {
            foreach(Bullet bullet in bulletsToRemove)
            {
                bullets.Remove(bullet);
            }
            bulletsToRemove.Clear();
        }

        private void CheckCollisions(Bullet bullet)
        {
            int radius = bullet.GetRadius();

            bool outOfBoundsX = bullet.GetPosition().X < 0 || bullet.GetPosition().X > Game1.WIDTH;
            bool outOfBoundsY = bullet.GetPosition().Y < 0 || bullet.GetPosition().Y > Game1.HEIGHT;

            if (outOfBoundsX || outOfBoundsY)
            {
                DeleteBullet(bullet);
         
            }
            else
            {
                for (int i = 0; i < enemyManager.GetNumEnemies(); i++)
                {
                    Enemy enemy = enemyManager.GetEnemy(i);
                   
                    if (Vector2.Distance(bullet.GetPosition(), enemy.GetPosition()) <= radius + enemy.GetRadius())
                    {
                        
                        enemy.TakeDamage(bullet.GetDamage());
                        DeleteBullet(bullet);
                        break;
                    }
                }
            }
        }

        public void Update()
        {
            foreach (Bullet bullet in bullets)
            {
               
                bullet.Update();
               
                CheckCollisions(bullet);
                
            }
            RemoveAtEndOfFrame();
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Bullet bullet in bullets)
            {
                bullet.Draw(batch, SpriteManager.GetSprite("bullet"));
            }
        }

        public int GetNumBullets()
        {
            return bullets.Count;
        }
    }
}
