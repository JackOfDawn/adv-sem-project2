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
    public class Player : Observer
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

            InitToZero();

            SetStartWeapon();
        }

        public Player(int health, float speed, Vector2 position)
        {
            this.health = health;
            this.speed = speed;
            this.position = position;

            this.center = position;

            InitToZero();

            SetStartWeapon();
        }

        public Player(int health, float speed, float x, float y)
        {
            this.health = health;
            this.speed = speed;
            this.position = new Vector2(x, y);

            this.center = new Vector2(x, y);

            InitToZero();
            SetStartWeapon();
        }

        public void setBulletmanager(BulletManager bulletManager)
        {
            bulletManagerRef = bulletManager;
        }

        private void SetStartWeapon()
        {
            currentWeapon = new ShotGun();
        }

        private void InitToZero()
        {
            height = 0;
            width = 0;

            origin = new Vector2();

            rotation = 0;
            damaged = false;
            damageCoolDownTimer = 0;
        }

        public void LoadSprite(Texture2D texture = null)
        {
            //multiple options
            if (texture == null)
            {
                this.texture = SpriteManager.GetSprite("player");
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

        public void Update(GameTime gameTime)
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

        public void UpdateMovement(Vector2 moveVector)
        {
            position += (moveVector * speed);
        }

        public void SwitchWeapons()
        {
            //not yet implemented
        }

        public void Shoot()
        {
            Vector2 direction = new Vector2((float)Math.Cos((double)rotation), (float)Math.Sin((double)rotation));

            currentWeapon.Shoot(this.position, direction, bulletManagerRef);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            if(!damaged)
                batch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            else
                batch.Draw(texture, position, null, Color.Red, rotation, origin, 1.0f, SpriteEffects.None, 0f);
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

        public void SetRotation(float newRotation)
        {
            rotation = newRotation;
        }

        public void SetRotation(Vector2 cursorLocation)
        {

            float xDiff = cursorLocation.X - position.X;
            float yDiff = cursorLocation.Y - position.Y;

            float SewRotation = (float)Math.Atan2(yDiff, xDiff);

            SetRotation(SewRotation);
        }

        public void SetHealth(int newHealth)
        {
            health = newHealth;
        }

        public void TakeDamage(float damage)
        {
            damaged = true;
            damageCoolDownTimer = DAMAGE_COOL_DOWN;
            health -= damage;
        }

        public bool IsDamaged()
        {
            return damaged;
        }

        public float GetHealth()
        {
            return health;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public float GetRadius()
        {
            return width / 2;
        }


        public void Notify(ObserverMessages message, int value = 0, Vector2 pos = new Vector2())
        {
            if(message == ObserverMessages.AMMO_PICKUP_MESSAGE)
            {
                currentWeapon.AddAmmo(value);
            }
        }
    }
}