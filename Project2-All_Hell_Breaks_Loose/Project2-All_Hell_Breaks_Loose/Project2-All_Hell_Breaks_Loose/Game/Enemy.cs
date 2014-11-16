using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public interface Enemy
    {
        void loadSprite(Texture2D texture);

        void update(Vector2 playerPos);
        void draw(SpriteBatch batch);

        void setHealth(int newHealth);
        float getHealth();
        void takeDamage(float damage);
        void setDamage(float damage);
        float getDamage();
        void setSpeed(float newSpeed);
        float getSpeed();
        Vector2 getPosition();
        int getHeight();
        int getWidth();

        int getRadius();
        Rectangle getBoundingRectangle();
    }
}
