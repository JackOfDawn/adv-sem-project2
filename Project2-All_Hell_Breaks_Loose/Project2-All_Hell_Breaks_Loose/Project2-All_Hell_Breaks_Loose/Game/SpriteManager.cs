using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class SpriteManager
    {
        private static Dictionary<string, Texture2D> sprites;

        static SpriteManager()
        {
            sprites = new Dictionary<string, Texture2D>();
        }

        public static void loadSprite(string key, Texture2D sprite)
        {
            //check to see if the specified key already exists
            if(!sprites.ContainsKey(key))
            {
                //if not add it
                sprites.Add(key, sprite);
            }
            else
            {
                //if so then throw error
                Console.WriteLine("Error, that key already exists");
            }
        }

        public static Texture2D getSprite(string key)
        {
            if(sprites.ContainsKey(key))
            {
                return sprites[key];
            }
            else
            {
                Console.WriteLine("Error, key does not exist");
                return null;
            }
        }
    }
}
