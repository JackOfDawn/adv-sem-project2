using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Strategies
{
    public class FleeMovement : MovementStrategy
    {
        
        const int THRESHOLD = 100;
        const float MAX_WANDER_ROTATION = 1.0f; //maximum rate of angle
        const float WANDER_FORWARD_OFFSET = 2.0f;
        const float WANDER_RADIUS = 10.0f;


        public Vector2 Update(Vector2 position, Vector2 targetLoc, float speed)
        {
            //if (Vector2.Distance(position, targetLoc) > 100)
            //'    return new SeekMovement().Update(position, targetLoc, speed);
            return Flee(position, targetLoc, speed);
        }

        private Vector2 Flee(Vector2 position, Vector2 targetLoc, float speed)
        {
            float T = speed / Vector2.Distance(position, targetLoc);
            Vector2 nextLocation = new Vector2();
            nextLocation = position - targetLoc;
            nextLocation.Normalize();

            nextLocation = position + nextLocation;

            return nextLocation;

        }

    }
}
