using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class Bullet
    {
        private float speed;
        private Vector2 direction;
        private int damage;
        private int radius;
        private Vector2 position;

        public Bullet(Vector2 position,float speed, Vector2 direction, int damage)
        {
            this.speed = speed;
            direction.Normalize();
            this.direction = direction;
            this.damage = damage;
            if (SpriteManager.GetSprite("bullet") != null)
            {
                this.radius = SpriteManager.GetSprite("bullet").Height/2;
            }
            else
            {
                this.radius = 5;
            }
            this.position = position;
        }

        public void Update()
        {
            position += (direction * speed);// * gameTime.ElapsedGameTime.Seconds);
            
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public int GetRadius()
        {
            return radius;
        }

        public Vector2 GetOrigin()
        {
            return position;
        }

        public float GetDamage()
        {
            return damage;
        }
    }
}
