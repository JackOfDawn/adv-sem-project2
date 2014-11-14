using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public interface Enemy
    {
        void loadSprite(Texture2D texture);

        void update();
        void draw(SpriteBatch batch);

        void setHealth(int newHealth);
        int getHealth();
        void setDamage(float newDamage);
        float getDamage();
        void setSpeed(float newSpeed);
        float getSpeed();
    }
}
