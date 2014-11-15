using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class EnemyFactory
    {
        //Minion Parameters
        private const int MINION_HEALTH = 50;
        private const float MINION_DAMAGE = 2;
        private const float MINION_SPEED = 2.0f;
        private static Vector2 ZERO = new Vector2(0, 0);

        public static Minion makeChaser()
        {
            Minion minion = new Minion(MINION_HEALTH, MINION_DAMAGE, MINION_SPEED, ZERO, new Strategies.SeekMovement());
            minion.loadSprite();
            return minion;
        }
    }
}
