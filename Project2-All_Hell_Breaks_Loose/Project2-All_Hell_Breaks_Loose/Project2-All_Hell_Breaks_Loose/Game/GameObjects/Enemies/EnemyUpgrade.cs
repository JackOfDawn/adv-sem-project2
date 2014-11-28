using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies
{
    class EnemyUpgrade : Enemy
    {
        private Enemy baseEnemy;

        public EnemyUpgrade(Enemy enemy)
        {
            baseEnemy = enemy;
        }

        public float GetHealth()
        {
            return baseEnemy.GetHealth() + 1;
        }

        public float GetDamage()
        {
            return baseEnemy.GetDamage() + 1;
        }

        public float GetSpeed()
        {
            return baseEnemy.GetSpeed() + 10;
        }

        public void Update(Microsoft.Xna.Framework.Vector2 playerPos)
        {
            baseEnemy.Update(playerPos);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            baseEnemy.Draw(batch);
        }

        public void SetHealth(int newHealth)
        {
            baseEnemy.SetHealth(newHealth);
        }

        public void TakeDamage(float damage)
        {
            baseEnemy.TakeDamage(damage);
        }

        public void SetDamage(float damage)
        {
            baseEnemy.SetDamage(damage);
        }

        public void SetSpeed(float newSpeed)
        {
            baseEnemy.SetSpeed(newSpeed);
        }

        public Microsoft.Xna.Framework.Vector2 GetPosition()
        {
            return baseEnemy.GetPosition();
        }

        public void SetPosition(float x, float y)
        {
            baseEnemy.SetPosition(x, y);
        }

        public int GetHeight()
        {
            return baseEnemy.GetHeight();
        }

        public int GetWidth()
        {
            return baseEnemy.GetWidth();
        }

        public int GetRadius()
        {
            return baseEnemy.GetRadius();
        }

        public Microsoft.Xna.Framework.Rectangle GetBoundingRectangle()
        {
            return baseEnemy.GetBoundingRectangle();
        }
    }
}
