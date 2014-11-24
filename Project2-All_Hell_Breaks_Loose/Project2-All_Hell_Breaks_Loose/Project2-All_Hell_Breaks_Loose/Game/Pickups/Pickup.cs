using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Pickups
{
    public abstract class Pickup : Observable
    {

        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 origin;

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(texture, position, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, 0f);
            batch.End();
        }

        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
 
        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetRadius()
        {
            return texture.Width / 2;
        }
    }
}
