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
        private Vector2 position;
        private Vector2 origin;

        public AmmoPickup(Vector2 newPosition)
        {
            observers = new List<Observer>();
            position = newPosition;
        }

        public override void notifyObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.notify(ObserverMessages.AMMO_PICKUP_MESSAGE, 5);
            }
        }

        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(texture, position, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0f);
            batch.End();
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public float getRadius()
        {
            return texture.Width / 2;
        }
    }
}
