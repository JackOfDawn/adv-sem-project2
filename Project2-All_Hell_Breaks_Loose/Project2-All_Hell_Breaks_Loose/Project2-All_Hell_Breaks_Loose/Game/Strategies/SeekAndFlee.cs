using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Strategies
{
    class SeekAndFlee : MovementStrategy
    {
        private SeekMovement seek;
        private FleeMovement flee;
        private const int THRESHOLD = 100;

        public SeekAndFlee()
        {
            seek = new SeekMovement();
            flee = new FleeMovement();
        }

        public Vector2 Update(Vector2 position, Vector2 targetLoc, float speed)
        {
            if (Vector2.DistanceSquared(position, targetLoc) > THRESHOLD * THRESHOLD)
                return seek.Update(position, targetLoc, speed);
            return flee.Update(position, targetLoc, speed);
            
        }

    }
}
