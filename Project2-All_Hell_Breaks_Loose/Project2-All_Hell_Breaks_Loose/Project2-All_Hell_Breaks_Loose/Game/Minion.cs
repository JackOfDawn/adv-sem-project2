using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class Minion : Enemy
    {

        private float health;
        private float damage;
        private float speed;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 center;
        private Vector2 origin;
        private float rotation;//in radians
        private Color color;

        private int height;
        private int width;

        private Strategies.MovementStrategy moveStrategy;

        public Minion()
        {
            health = 0;
            damage = 0;
            speed = 0;
            position = new Vector2();
            center = new Vector2();
            color = Color.White;
            moveStrategy = null;

            initToZero();
        }

        public Minion(int health, float damage, float speed, Vector2 pos, Strategies.MovementStrategy strategy, Color color)
        {
            this.health = health;
            this.damage = damage;
            this.speed = speed;
            this.position = pos;
            this.center = pos;
            this.color = color;
            moveStrategy = strategy;

            initToZero();
        }

        public Minion(int health, float damage, float speed, float x, float y, Strategies.MovementStrategy strategy, Color color)
        {
            this.health = health;
            this.damage = damage;
            this.speed = speed;
            this.position = new Vector2(x, y);
            this.center = new Vector2(x, y);
            this.color = color;
            moveStrategy = strategy;

            initToZero();
        }

        private void initToZero()
        {
            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
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
            if(this.texture != null)
            {
                height = this.texture.Height;
                width = this.texture.Width;
                origin = new Vector2(width / 2, height / 2);
            }
        }

        public void update(Vector2 playerPos)
        {
            setRotation(playerPos);

            position = moveStrategy.update(position, playerPos, speed);
        }

        public void draw(SpriteBatch batch)
        {
            batch.Begin();
            
            batch.Draw(texture, position, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0f);

            batch.End();
        }

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
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

        public Vector2 getOrigin()
        {
            return origin;
        }

        public void setOrigin(Vector2 newOrigin)
        {
            origin = newOrigin;
        }

        public void setOrigin(float x, float y)
        {
            origin = new Vector2(x, y);
        }

        public void setRotation(float newRotation)
        {
            rotation = newRotation;
        }

        public void setRotation(Vector2 targetLocation)
        {

            float xDiff = targetLocation.X - position.X;
            float yDiff = targetLocation.Y - position.Y;

            float newRotation = (float)Math.Atan2(yDiff, xDiff);

            setRotation(newRotation);
        }

        public void setHealth(int newHealth)
        {
            health = newHealth;
        }

        public float getHealth()
        {
            return health;
        }

        public void takeDamage(float damage)
        {
            health -= damage;
        }
        public void setDamage(float damage)
        {
            this.damage = damage;
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

        public Rectangle getBoundingRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public int getHeight()
        {
            return height;
        }

        public int getWidth()
        {
            return width;
        }

        public void setHeight(int newHeight)
        {
            height = newHeight;
        }

        public void setWidth(int newWidth)
        {
            width = newWidth;
        }

        public Color getColor()
        {
            return color;
        }

        public int getRadius()
        {
            return (height + width) / 4;
        }
    }
}
