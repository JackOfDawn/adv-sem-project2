using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies
{
    public class EnemyFactory
    {
        //Minion Parameters
        private const int MINION_HEALTH = 50;
        private const float MINION_DAMAGE = 2;
        private const float MINION_SPEED = 2.0f;
        private static Vector2 ZERO = new Vector2(0, 0);

        public static Color CHASER_COLOR = Color.Tomato;
        public static Color FLEEING_COLOR = Color.Gainsboro;
        public static Color BLOCKER_COLOR = Color.LimeGreen;

        public static Minion makeChaser()
        {
            return baseMinion(new Strategies.SeekMovement(), CHASER_COLOR);
        }

        public static Minion makeFleeing()
        {
            return baseMinion(new Strategies.FleeMovement(), FLEEING_COLOR);
        }

        public static Minion makeBlocker()
        {
            return baseMinion(new Strategies.SeekAndFlee(), BLOCKER_COLOR);
        }
        
        
        private static Minion baseMinion(Strategies.MovementStrategy strategy, Color color)
        {
            Minion minion = new Minion(MINION_HEALTH, MINION_DAMAGE, MINION_SPEED, ZERO, strategy, color);
            minion.LoadSprite("enemy");
            return minion;
        }
    }
}
