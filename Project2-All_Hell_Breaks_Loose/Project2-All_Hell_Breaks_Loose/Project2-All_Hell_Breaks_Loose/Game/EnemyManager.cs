using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class EnemyManager : Observable
    {
        private List<Enemy> enemies;
        private List<Enemy> enemiesToDelete;


        public EnemyManager()
        {
            enemies = new List<Enemy>();
            enemiesToDelete = new List<Enemy>();
            observers = new List<Observer>();
        }
        ~EnemyManager()
        {
        }

        public void addEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void addEnemies(List<Enemy> enemiesToAdd)
        {
            enemies.AddRange(enemiesToAdd);
        }

        public void deleteEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public void deleteEnemy(int index)
        {
            enemies.RemoveAt(index);
        }

        public void update(Microsoft.Xna.Framework.Vector2 playerPos)
        {
        
            foreach(Enemy enemy in enemies)
            {
                enemy.update(playerPos);
                if(enemy.getHealth() <= 0)
                {
                    enemiesToDelete.Add(enemy);
                    
                }
            }

            notifyObservers();
            removeAtEndOfFrame();
        }

        public void checkPlayerCollision(Player player)
        {
            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(enemy.getPosition(), player.getPosition()) < player.getRadius() + enemy.getRadius())
                {
                    if(player.isDamaged() == false)
                        player.takeDamage(enemy.getDamage());
                    
                }
            }
        }

        private void removeAtEndOfFrame()
        {
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
            }
            enemiesToDelete.Clear();
        }

        public void draw(SpriteBatch batch)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.draw(batch);
            }
        }
        public int getNumEnemies()
        {
            return enemies.Count;
        }

        public Enemy getEnemy(int index)
        {
            return enemies[index];
        }

        public override void notifyObservers()
        {
            foreach (Observer observer in observers)
            {
                foreach (Enemy enemy in enemiesToDelete)
                {
                    observer.notify(ObserverMessages.SPAWN_PICKUPS_MESSAGE, 0, enemy.getPosition());
                    
                }
                
            }
        }
    }
}
