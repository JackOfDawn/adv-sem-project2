using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project2_All_Hell_Breaks_Loose.Game.Managers;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class DeathScreen :GameObjects.GameObject
    {
        bool draw;

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
                batch.Draw(texture, origin, Color.White);
                batch.End();
            }
        }
    }
}
