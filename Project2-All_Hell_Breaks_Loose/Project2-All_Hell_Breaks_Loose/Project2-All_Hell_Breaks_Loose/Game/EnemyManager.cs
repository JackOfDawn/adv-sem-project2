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

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void AddEnemies(List<Enemy> enemiesToAdd)
        {
            enemies.AddRange(enemiesToAdd);
        }

        public void DeleteEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public void DeleteEnemy(int index)
        {
            enemies.RemoveAt(index);
        }

        public void Update(Microsoft.Xna.Framework.Vector2 playerPos)
        {
        
            foreach(Enemy enemy in enemies)
            {
                enemy.Update(playerPos);
                if(enemy.GetHealth() <= 0)
                {
                    enemiesToDelete.Add(enemy);
                    
                }
            }

            NotifyObservers();
            RemoveAtEndOfFrame();
        }

        public void CheckPlayerCollision(Player player)
        {
            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(enemy.GetPosition(), player.GetPosition()) < player.GetRadius() + enemy.GetRadius())
                {
                    if(player.IsDamaged() == false)
                        player.TakeDamage(enemy.GetDamage());
                    
                }
            }
        }

        private void RemoveAtEndOfFrame()
        {
            foreach (Enemy enemy in enemiesToDelete)
            {
                enemies.Remove(enemy);
            }
            enemiesToDelete.Clear();
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(batch);
            }
        }
        public int GetNumEnemies()
        {
            return enemies.Count;
        }

        public Enemy GetEnemy(int index)
        {
            return enemies[index];
        }

        public override void NotifyObservers()
        {
            foreach (Observer observer in observers)
            {
                foreach (Enemy enemy in enemiesToDelete)
                {
                    observer.Notify(ObserverMessages.SPAWN_PICKUPS_MESSAGE, 0, enemy.GetPosition());
                    
                }
                
            }
        }
    }
}
