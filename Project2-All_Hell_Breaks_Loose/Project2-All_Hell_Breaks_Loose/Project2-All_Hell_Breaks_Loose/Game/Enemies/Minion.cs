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
            : this(0, 0, 0, new Vector2(), new Strategies.NullMovement(), Color.White) { }

        
        public Minion(int health, float damage, float speed, Vector2 pos, Strategies.MovementStrategy strategy, Color color)
        {
            this.health = health;
            this.damage = damage;
            this.speed = speed;
            this.position = pos;
            this.center = pos;
            this.color = color;
            moveStrategy = strategy;

            InitToZero();
        }

        public Minion(int health, float damage, float speed, float x, float y, Strategies.MovementStrategy strategy, Color color)
            : this(health, damage, speed, new Vector2(x, y), strategy, color) { }

        private void InitToZero()
        {
            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
        }

        ~Minion()
        {
        }

        public void LoadSprite(Texture2D texture = null)
        {
            //multiple options
            if(texture == null)
            {
               this.texture = SpriteManager.GetSprite("enemy");
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

        public void Update(Vector2 playerPos)
        {
            SetRotation(playerPos);

            position = moveStrategy.Update(position, playerPos, speed);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            
            batch.Draw(texture, position, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0f);

            batch.End();
        }

        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetCenter()
        {
            return center;
        }

        public Vector2 GetOrigin()
        {
            return origin;
        }

        public void SetOrigin(Vector2 newOrigin)
        {
            origin = newOrigin;
        }

        public void SetOrigin(float x, float y)
        {
            origin = new Vector2(x, y);
        }

        public void SetRotation(float newRotation)
        {
            rotation = newRotation;
        }

        public void SetRotation(Vector2 targetLocation)
        {

            float xDiff = targetLocation.X - position.X;
            float yDiff = targetLocation.Y - position.Y;

            float newRotation = (float)Math.Atan2(yDiff, xDiff);

            SetRotation(newRotation);
        }

        public void SetHealth(int newHealth)
        {
            health = newHealth;
        }

        public float GetHealth()
        {
            return health;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
        }
        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        public float GetDamage()
        {
            return damage;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public Rectangle GetBoundingRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public int GetHeight()
        {
            return height;
        }

        public int GetWidth()
        {
            return width;
        }

        public void SetHeight(int newHeight)
        {
            height = newHeight;
        }

        public void SetWidth(int newWidth)
        {
            width = newWidth;
        }

        public Color GetColor()
        {
            return color;
        }

        public int GetRadius()
        {
            return (height + width) / 4;
        }
    }
}
