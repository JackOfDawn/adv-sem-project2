using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
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

        public void addBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        public void addBullets(List<Bullet>bulletsToAdd)
        {
            bullets.AddRange(bulletsToAdd);
        }

        public void deleteBullet(Bullet bullet)
        {
            bulletsToRemove.Add(bullet);
        }

        public void deleteBullet(int index)
        {
            bulletsToRemove.Add(bullets[index]);
        }

        private void removeAtEndOfFrame()
        {
            foreach(Bullet bullet in bulletsToRemove)
            {
                bullets.Remove(bullet);
            }
            bulletsToRemove.Clear();
        }

        private void checkCollisions(Bullet bullet)
        {
            int radius = bullet.getRadius();

            bool outOfBoundsX = bullet.getPosition().X < 0 || bullet.getPosition().X > Game1.WIDTH;
            bool outOfBoundsY = bullet.getPosition().Y < 0 || bullet.getPosition().Y > Game1.HEIGHT;

            if (outOfBoundsX || outOfBoundsY)
            {
                deleteBullet(bullet);
         
            }
            else
            {
                for (int i = 0; i < enemyManager.getNumEnemies(); i++)
                {
                    Enemy enemy = enemyManager.getEnemy(i);
                   
                    if (Vector2.Distance(bullet.getPosition(), enemy.getPosition()) <= radius + enemy.getRadius())
                    {
                        
                        enemy.takeDamage(bullet.getDamage());
                        deleteBullet(bullet);
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
               
                checkCollisions(bullet);
                
            }
            removeAtEndOfFrame();
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Bullet bullet in bullets)
            {
                bullet.Draw(batch, SpriteManager.getSprite("bullet"));
            }
        }

        public int getNumBullets()
        {
            return bullets.Count;
        }
    }
}
