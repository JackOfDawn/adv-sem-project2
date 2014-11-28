using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project2_All_Hell_Breaks_Loose.Game.Weapons;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;

using Project2_All_Hell_Breaks_Loose.Game.Managers;

namespace Project2_All_Hell_Breaks_Loose.Game.GameObjects
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

        private Dictionary<int, Weapon> weaponlist;
        private int weaponNum;
        private Weapon currentWeapon;
        private BulletManager bulletManagerRef;

        public Player() 
            : this(0, 0, new Vector2()) { }

        public Player(int health, float speed, float x, float y)
            : this (health, speed, new Vector2(x, y)) {}

        public Player(int health, float speed, Vector2 position)
        {
            this.health = health;
            this.speed = speed;
            this.position = position;
            this.center = position;

            InitToZero();
            SetStartWeapon();
        }


        public void setBulletmanager(BulletManager bulletManager)
        {
            bulletManagerRef = bulletManager;
        }

        private void SetStartWeapon()
        {
            weaponNum = 0;

            weaponlist = new Dictionary<int, Weapon>();

            Weapon weaponToAdd = new Pistol();
            weaponlist.Add(0, weaponToAdd);

            weaponToAdd = new ShotGun();
            weaponlist.Add(1, weaponToAdd);

            currentWeapon = weaponlist[0];
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
            weaponNum++;
            if(weaponNum >= weaponlist.Count)
            {
                weaponNum = 0;
            }

            currentWeapon = weaponlist[weaponNum];
        }

        public void Shoot()
        {
            Vector2 direction = new Vector2((float)Math.Cos((double)rotation), (float)Math.Sin((double)rotation));

            currentWeapon.Shoot(this.position, direction, bulletManagerRef);
        }

        public void Draw(SpriteBatch batch)
        {
            Color drawcolor;
            batch.Begin();
            if(!damaged)
                drawcolor = Color.White;
            else
                drawcolor = Color.Red;
                
            batch.Draw(texture, position, null, drawcolor, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            batch.End();
        }

        public void upgradePistol()
        {
            weaponlist[0] = new DecoratedPistol(weaponlist[0] as AbstractPistol);
            if(weaponNum == 0)
            {
                currentWeapon = weaponlist[0];
            }
        }

        public void upgradeShotgun()
        {
            weaponlist[1] = new DecoratedShotgun(weaponlist[1] as AbstractShotGun);
            if (weaponNum == 1)
            {
                currentWeapon = weaponlist[1];
            }
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

        public string getWeaponName()
        {
            return currentWeapon.ClassName();
        }

        public int getAmmo()
        {
            return currentWeapon.getAmmo();
        }

        public void giveAmmo(int ammo)
        {
            currentWeapon.AddAmmo(ammo);
        }

        public void Notify(ObserverMessages message, int value = 0, Vector2 pos = new Vector2())
        {
            if(message == ObserverMessages.AMMO_PICKUP_MESSAGE)
            {
                if(weaponNum == 0)
                {
                    weaponlist[1].AddAmmo(value);
                }
                else
                {
                    currentWeapon.AddAmmo(value);
                }
            }
        }
    }
}