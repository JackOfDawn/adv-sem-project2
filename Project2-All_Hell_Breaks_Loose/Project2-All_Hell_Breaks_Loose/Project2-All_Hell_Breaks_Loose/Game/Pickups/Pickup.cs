using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game.Pickups
{
    interface Pickup
    {
        void Draw(SpriteBatch batch);
        void LoadTexture(Texture2D texture);
        Vector2 GetPosition();
        float GetRadius();
    }
}
