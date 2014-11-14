using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;



namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class EnemyManager
    {
        private List<Enemy> enemies;

        public EnemyManager()
        {
            enemies = new List<Enemy>();
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

        public void update()
        {
        
            foreach(Enemy enemy in enemies)
            {
                enemy.update();
            }
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

    }
}
