using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public enum ObserverMessages
    {
        AMMO_PICKUP_MESSAGE = 0,
        MONEY_PICKUP_MESSAGE,
        SPAWN_PICKUPS_MESSAGE
    }

    public interface Observer
    {
        void Notify(ObserverMessages message, int value = 0, Vector2 pos = new Vector2());
    }
}
