using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Project2_All_Hell_Breaks_Loose.Game.Managers;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class Shop : GameObject
    {
        bool shopOpen;

        public Shop(Vector2 position)
        {
            shopOpen = false;
            this.position = position;
        }

        public void openShop()
        {
            shopOpen = true;
        }

        public void closeShop()
        {
            shopOpen = false;
        }

        public void Draw(SpriteBatch batch)
        {
            if(shopOpen)
            {
                batch.Begin();
                batch.Draw(texture, origin, Color.White);
                batch.End();
            }
        }

        public bool isOpen()
        {
            return shopOpen;
        }
    }
}
