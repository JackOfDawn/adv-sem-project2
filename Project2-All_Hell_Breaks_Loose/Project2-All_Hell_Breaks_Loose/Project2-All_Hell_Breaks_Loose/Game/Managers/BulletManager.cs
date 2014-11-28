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
    public class BulletManager : GenericManager<Bullet>
    {
        private EnemyManager enemyManager;

        public BulletManager(EnemyManager enemyManagerRef)
        {
            enemyManager = enemyManagerRef;
        }

       

        private void CheckCollisions(Bullet bullet)
        {
            int radius = bullet.GetRadius();

            bool outOfBoundsX = bullet.GetPosition().X < 0 || bullet.GetPosition().X > Game1.WIDTH;
            bool outOfBoundsY = bullet.GetPosition().Y < 0 || bullet.GetPosition().Y > Game1.HEIGHT;

            if (outOfBoundsX || outOfBoundsY)
            {
                
                DeleteObject(bullet);
         
            }
            else
            {
                for (int i = 0; i < enemyManager.GetCount(); i++)
                {
                    Enemy enemy = enemyManager.GetObject(i);
                   
                    if (Vector2.Distance(bullet.GetPosition(), enemy.GetPosition()) <= radius + enemy.GetRadius())
                    {
                        
                        enemy.TakeDamage(bullet.GetDamage());
                        DeleteObject(bullet);
                        break;
                    }
                }
            }
        }

        public void Update()
        {
            foreach (Bullet bullet in objects)
            {
               
                bullet.Update();
               
                CheckCollisions(bullet);
                
            }
        
            RemoveAtEndOfFrame();
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Bullet bullet in objects)
            {
                bullet.Draw(batch);
            }
        }


    }
}
