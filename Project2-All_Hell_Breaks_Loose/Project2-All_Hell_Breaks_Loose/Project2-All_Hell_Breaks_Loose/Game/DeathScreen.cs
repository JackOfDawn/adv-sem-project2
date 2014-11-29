using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project2_All_Hell_Breaks_Loose.Game.Managers;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class DeathScreen
    {
        bool draw;
        Texture2D texture;
        Vector2 position;

        public DeathScreen(Vector2 pos)
        {
            position = pos;
            draw = false;
        }

        public void setDraw(bool setTo)
        {
            draw = setTo;
        }

        public void Draw(SpriteBatch batch)
        {
            if(draw)
            {
                batch.Begin();
                batch.Draw(texture, position, Color.White);
                batch.End();
            }
        }

        public void LoadSprite(Texture2D texture = null)
        {
            //multiple options
            if (texture == null)
            {
                this.texture = SpriteManager.GetSprite("death");
            }
            else
            {
                this.texture = texture;
            }
            position.X -= this.texture.Width / 2;
            position.Y -= this.texture.Height / 2;
        }
    }
}
