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
    public class EnemyManager : GenericManager<Enemy>
    {

        public EnemyManager()
        {
            observers = new List<Observer>();
        }

        ~EnemyManager()
        {
        }

        public void Update(Microsoft.Xna.Framework.Vector2 playerPos)
        {
        
            foreach(Enemy enemy in objects)
            {
                enemy.Update(playerPos);
                if(enemy.GetHealth() <= 0)
                {
                    objectsToRemove.Add(enemy);
                }
            }

            NotifyObservers();
            RemoveAtEndOfFrame();
        }

        public void CheckPlayerCollision(Player player)
        {
            foreach (Enemy enemy in objects)
            {
                if (Vector2.Distance(enemy.GetPosition(), player.GetPosition()) < player.GetRadius() + enemy.GetRadius())
                {
                    if(player.IsDamaged() == false)
                        player.TakeDamage(enemy.GetDamage());
                    
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Enemy enemy in objects)
            {
                enemy.Draw(batch);
            }
        }

        public override void NotifyObservers()
        {
            foreach (Observer observer in observers)
            {
                foreach (Enemy enemy in objectsToRemove)
                {
                    observer.Notify(ObserverMessages.SPAWN_PICKUPS_MESSAGE, 0, enemy.GetPosition());
                }
            }
        }
    }
}
