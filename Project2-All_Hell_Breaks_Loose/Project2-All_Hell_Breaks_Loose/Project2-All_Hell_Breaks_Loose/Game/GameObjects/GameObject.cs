using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.Managers;
namespace Project2_All_Hell_Breaks_Loose.Game.GameObjects
{
    public abstract class GameObject
    {
        protected Texture2D texture;
        protected int height;
        protected int width;
        protected Vector2 origin;
        protected Vector2 position;
        protected Vector2 center;

        public void LoadSprite(string textureName, Texture2D texture = null)
        {
            //multiple options
            if (texture == null)
            {
                this.texture = SpriteManager.GetSprite(textureName);
            }
            else
            {
                this.texture = texture;
            }

            //set the height and width and center according to the size of the sprite
            if(this.texture != null)
            {
                height = this.texture.Height;
                width = this.texture.Width;

                origin = new Vector2(width / 2, height / 2);
            }
        }
    }
}
