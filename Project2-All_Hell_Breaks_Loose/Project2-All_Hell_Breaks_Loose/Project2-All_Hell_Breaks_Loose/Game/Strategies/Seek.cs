using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Strategies
{
    public class SeekMovement : MovementStrategy
    {
        public Vector2 update(Vector2 position, Vector2 targetLoc, float speed)
        {
            return Seek(position, targetLoc, speed);
        }

        private Vector2 Seek(Vector2 position, Vector2 targetLoc, float speed)
        {
            //self
            float a = position.X;
            float b = position.Y;

            //target
            float c = targetLoc.X;
            float d = targetLoc.Y;

            //calc distance between the two points
            float distance = (float)Math.Sqrt(Math.Pow((c - a), 2) + Math.Pow((d - b), 2));

            float T = speed / distance;

            //finding target point
            Vector2 nextLocation = new Vector2();

            nextLocation.X = (1 - T) * a + T * c;
            nextLocation.Y = (1 - T) * b + T * d;

            return nextLocation;
        }
    }
}
