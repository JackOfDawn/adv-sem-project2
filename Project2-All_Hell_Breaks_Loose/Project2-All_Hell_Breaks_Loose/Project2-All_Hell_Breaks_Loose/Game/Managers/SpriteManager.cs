using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project2_All_Hell_Breaks_Loose.Game.Managers
{
    public class SpriteManager
    {
        private static Dictionary<string, Texture2D> sprites;
        private const string missingTextureName = "missing";

        static SpriteManager()
        {
            sprites = new Dictionary<string, Texture2D>();
        }

        public static void GenerateDefaultTexture(GraphicsDevice device)
        {
            int textureHeight = 50;
            int textureWidth = 50;
            int textureSize = textureHeight * textureWidth;

            Color[] texture = new Color[textureSize];

            for(int i = 0; i < textureSize; i ++)
            {
                texture[i] = Color.Gray;
            }

            Texture2D missingTexture = new Texture2D(device, textureHeight, textureWidth);
            missingTexture.SetData(texture);

            sprites.Add(missingTextureName, missingTexture);
        }

        public static void LoadSprite(string key, Texture2D sprite)
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

        public static Texture2D GetSprite(string key)
        {
            if(sprites.ContainsKey(key))
            {
                return sprites[key];
            }
            else if (sprites.ContainsKey(missingTextureName))
            {
                return sprites[missingTextureName];
            }
            else
            {
                return null;
            }
        }
    }
}
