﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Strategies
{
    public interface MovementStrategy
    {
        Vector2 Update(Vector2 position, Vector2 targetLoc, float speed);
    }
}
