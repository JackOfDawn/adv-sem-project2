using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Strategies
{
    public class NullMovement : MovementStrategy
    {
        public Vector2 Update(Vector2 position = new Vector2(), Vector2 targetLoc = new Vector2(), float speed = 1)
        {
            return position;
        }
    }
}
