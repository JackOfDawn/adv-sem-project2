using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Project2_All_Hell_Breaks_Loose.Game.Strategies
{
    public class BansheeMovment : MovementStrategy
    {
        private SeekMovement seek;
        private const int THRESHOLD = 45;
        public BansheeMovment()
        {
            seek = new SeekMovement();
        }

        public Vector2 Update(Vector2 position, Vector2 targetLoc, float speed)
        {
            Vector2 newLoc = Vector2.Zero;
            if (Vector2.DistanceSquared(position, targetLoc) > THRESHOLD * THRESHOLD)
                return seek.Update(position, targetLoc, speed * 5);

            Random rand = new Random((int)(position.X));
            rand.Next(10);
            if(rand.Next(10) <= 1)
                return seek.Update(position, targetLoc, speed * 5);
            else
                switch(rand.Next(4))
                {
                    case 0:
                        newLoc = Vector2.Zero;
                        break;
                    case 1:
                        newLoc = new Vector2(Game1.WIDTH, Game1.HEIGHT);
                        break;
                    case 2:
                        newLoc = new Vector2(Game1.WIDTH, 0);
                        break;
                    default:
                        newLoc = new Vector2(0, Game1.HEIGHT);
                        break;
                }

            return newLoc;
        }
    }
}
