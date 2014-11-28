using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Project2_All_Hell_Breaks_Loose.Game.Managers;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class Shop
    {
        bool shopOpen;
        Texture2D texture;
        Vector2 position;

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

        public void update()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.E))
            {
                closeShop();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(shopOpen)
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
                this.texture = SpriteManager.GetSprite("shop");
            }
            else
            {
                this.texture = texture;
            }
            position.X -= this.texture.Width / 2;
            position.Y -= this.texture.Height / 2;
        }

        public bool isOpen()
        {
            return shopOpen;
        }
    }
}
