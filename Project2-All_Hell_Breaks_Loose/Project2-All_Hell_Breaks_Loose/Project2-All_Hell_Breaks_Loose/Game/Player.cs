using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class Player
    {
        private int health;
        private float speed;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 center;
        private Vector2 origin;
        private float rotation;//in radians

        private int height;
        private int width;
    

        public Player()
        {
            health = 0;
            speed = 0;
            position = new Vector2();

            center = new Vector2();

            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
        }

        public Player(int health, float speed, Vector2 position)
        {
            this.health = health;
            this.speed = speed;
            this.position = position;

            this.center = position;

            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
        }

        public Player(int health, float speed, float x, float y)
        {
            this.health = health;
            this.speed = speed;
            this.position = new Vector2(x, y);

            this.center = new Vector2(x, y);

            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
        }

        public void loadSprite(Texture2D texture = null)
        {
            //multiple options
            if (texture == null)
            {
                this.texture = SpriteManager.getSprite("player");
            }
            else
            {
                this.texture = texture;
            }

            //set the height and width and center according to the size of the sprite
            height = this.texture.Height;
            width = this.texture.Width;

            origin = new Vector2(width / 2, height / 2);
        }

        public void update()
        {
            
        }

        public void updateMovement(Vector2 moveVector)
        {
            position += (moveVector * speed);
        }

        public void switchWeapons()
        {

        }


        public void draw(SpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
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

        public void setRotation(float newRotation)
        {
            rotation = newRotation;
        }

        public void setRotation(Vector2 cursorLocation)
        {
            float rotationOffset = (float)Math.PI / 2;

            float xDiff = position.X - cursorLocation.X;
            float yDiff = position.Y - cursorLocation.Y;

            float newRotation = (float)Math.Atan2(yDiff, xDiff);

            setRotation(newRotation - rotationOffset);
        }

        public void setHealth(int newHealth)
        {
            health = newHealth;
        }

        public int getHealth()
        {
            return health;
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