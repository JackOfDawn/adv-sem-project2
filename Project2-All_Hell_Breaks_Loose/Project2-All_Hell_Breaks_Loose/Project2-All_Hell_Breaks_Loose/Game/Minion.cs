using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class Minion : Enemy
    {

        private int health;
        private float damage;
        private float speed;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 center;

        private int height;
        private int width;

        public Minion()
        {
            health = 0;
            damage = 0;
            speed = 0;
            position = new Vector2(0, 0);
            center = new Vector2(0, 0);

            height = 0;
            width = 0;
        }

        public Minion(int health, float damage, float speed, Vector2 pos)
        {
            this.health = health;
            this.damage = damage;
            this.speed = speed;
            this.position = pos;
            this.center = pos;

            height = 0;
            width = 0;
        }

        ~Minion()
        {
        }

        public void loadSprite(Texture2D texture = null)
        {
            //multiple options
            if(texture == null)
            {
                this.texture = SpriteManager.getSprite("enemy");
            }
            else
            {
                this.texture = texture;
            }

            //set the height and width and center according to the size of the sprite
            height = this.texture.Height;
            width = this.texture.Width;
            position.X -= width / 2;
            position.Y -= height / 2;
        }

        public void update()
        {
            position.X += speed;
            center.X += speed;
        }

        public void draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(texture, center, Color.White);
            batch.End();
        }

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;

            center.X = x + width / 2;
            center.Y = y + height / 2;
        }

        public void setPosition(Vector2 pos)
        {
            position = pos;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getCenter()
        {
            return center;
        }

        public void setHealth(int newHealth)
        {
            health = newHealth;
        }

        public int getHealth()
        {
            return health;
        }

        public void setDamage(float newDamage)
        {
            damage = newDamage;
        }

        public float getDamage()
        {
            return damage;
        }

        public void setSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        public float getSpeed()
        {
            return speed;
        }
    }
}
