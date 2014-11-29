using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.Managers;

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

        public void LoadSprite(Texture2D texture = null)
        {
            //multiple options
            if (texture == null)
            {
                this.texture = SpriteManager.GetSprite("ammo");
            }
            else
            {
                this.texture = texture;
            }
            if (this.texture != null)
            {
                position.X -= this.texture.Width / 2;
                position.Y -= this.texture.Height / 2;
            }
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
