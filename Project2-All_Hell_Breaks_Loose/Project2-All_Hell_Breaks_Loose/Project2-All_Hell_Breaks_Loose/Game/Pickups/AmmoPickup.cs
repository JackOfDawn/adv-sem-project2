using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Pickups
{
    class AmmoPickup : Observable, Pickup
    {
        private Texture2D texture;
        Vector2 position;

        public AmmoPickup()
        {
            observers = new List<Observer>();
        }

        public override void notifyObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.notify(0, 5);
            }
        }

        public void LoadTextured(Texture2D texture)
        {

        }

        public void Draw(SpriteBatch batch)
        {

        }
    }
}
