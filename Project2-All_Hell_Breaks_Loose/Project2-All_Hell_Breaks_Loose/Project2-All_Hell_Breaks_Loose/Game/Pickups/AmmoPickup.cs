using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Pickups
{
    class AmmoPickup : Pickup
    {


        public AmmoPickup(Vector2 newPosition)
        {
            observers = new List<Observer>();
            position = newPosition;
        }

        public override void NotifyObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.Notify(ObserverMessages.AMMO_PICKUP_MESSAGE, 5);
            }
        }
    }
}


