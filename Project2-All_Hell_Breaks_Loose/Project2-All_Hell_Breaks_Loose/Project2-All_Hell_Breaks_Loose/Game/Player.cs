using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project2_All_Hell_Breaks_Loose.Game.Weapons;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class Player
    {
        private float health;
        private float speed;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 center;
        private Vector2 origin;
        private float rotation;//in radians

        private int height;
        private int width;

        private bool damaged;
        private const int DAMAGE_COOL_DOWN = 500;
        private float damageCoolDownTimer;


        private Weapon currentWeapon;
        private BulletManager bulletManagerRef;

        public Player()
        {
            health = 0;
            speed = 0;
            position = new Vector2();

            center = new Vector2();

            initToZero();

            setStartWeapon();
        }

        public Player(int health, float speed, Vector2 position)
        {
            this.health = health;
            this.speed = speed;
            this.position = position;

            this.center = position;

            initToZero();

            setStartWeapon();
        }

        public Player(int health, float speed, float x, float y)
        {
            this.health = health;
            this.speed = speed;
            this.position = new Vector2(x, y);

            this.center = new Vector2(x, y);

            initToZero();
            setStartWeapon();
        }

        public void setBulletmanager(BulletManager bulletManager)
        {
            bulletManagerRef = bulletManager;
        }

        private void setStartWeapon()
        {
            currentWeapon = new DecoratedPistol(new Pistol());
        }

        private void initToZero()
        {
            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
            damaged = false;
            damageCoolDownTimer = 0;
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

        public void update(GameTime gameTime)
        {
            if (damaged)
            {
                damageCoolDownTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if(damageCoolDownTimer <= 0)
                {
                    damaged = false;
                    
                }
            }
        }

        public void updateMovement(Vector2 moveVector)
        {
            position += (moveVector * speed);
        }

        public void switchWeapons()
        {

        }

        public void Shoot()
        {
            Vector2 direction = new Vector2((float)Math.Cos((double)rotation), (float)Math.Sin((double)rotation));

            currentWeapon.shoot(this.position, direction, bulletManagerRef);
        }

        public void draw(SpriteBatch batch)
        {
            batch.Begin();
            if(!damaged)
                batch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            else
                batch.Draw(texture, position, null, Color.Red, rotation, origin, 1.0f, SpriteEffects.None, 0f);
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

            float xDiff = cursorLocation.X - position.X;
            float yDiff = cursorLocation.Y - position.Y;

            float newRotation = (float)Math.Atan2(yDiff, xDiff);

            setRotation(newRotation);
        }

        public void setHealth(int newHealth)
        {
            health = newHealth;
        }

        public void takeDamage(float damage)
        {
            damaged = true;
            damageCoolDownTimer = DAMAGE_COOL_DOWN;
            health -= damage;
        }

        public bool isDamaged()
        {
            return damaged;
        }

        public float getHealth()
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

        public float getRadius()
        {
            return width / 2;
        }
        
    }
}