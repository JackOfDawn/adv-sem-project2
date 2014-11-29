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
        public static Color BANSHEE_COLOR = Color.GhostWhite;

        public static Random rand = new Random();

        public static void SeedRandom(int seed)
        {
            rand = new Random(seed);
        }

        public static Minion MakeRandomMinion()
        {
            int selection = rand.Next(100);

            if(selection < 10)
            {
                return MakeBanshee();
            }
            else if( selection < 55)
            {
                return MakeBlocker();
            }
            else
            {
                return MakeChaser();
            }
            
        }

        public static Minion MakeChaser()
        {
            return BaseMinion(new Strategies.SeekMovement(), CHASER_COLOR);
        }

        public static Minion MakeFleeing()
        {
            return BaseMinion(new Strategies.FleeMovement(), FLEEING_COLOR);
        }

        public static Minion MakeBlocker()
        {
            return BaseMinion(new Strategies.SeekAndFlee(), BLOCKER_COLOR);
        }
        
        public static Minion MakeBanshee()
        {
            return BaseMinion(new Strategies.BansheeMovment(), BANSHEE_COLOR);
        }
        
        private static Minion BaseMinion(Strategies.MovementStrategy strategy, Color color)
        {
            Minion minion = new Minion(MINION_HEALTH, MINION_DAMAGE, MINION_SPEED, ZERO, strategy, color);
            minion.LoadSprite("enemy");
            return minion;
        }
    }
}
