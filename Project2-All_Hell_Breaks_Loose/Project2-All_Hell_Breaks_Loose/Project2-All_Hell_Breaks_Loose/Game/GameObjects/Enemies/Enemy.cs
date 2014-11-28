using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies
{
    public interface Enemy
    {
  

        void Update(Vector2 playerPos);
        void Draw(SpriteBatch batch);

        void SetHealth(int newHealth);
        float GetHealth();
        void TakeDamage(float damage);
        void SetDamage(float damage);
        float GetDamage();
        void SetSpeed(float newSpeed);
        float GetSpeed();
        Vector2 GetPosition();
        int GetHeight();
        int GetWidth();

        int GetRadius();
        Rectangle GetBoundingRectangle();
    }
}
