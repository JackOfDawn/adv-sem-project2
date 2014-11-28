using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Project2_All_Hell_Breaks_Loose.Game.Pickups;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;
using Project2_All_Hell_Breaks_Loose.Game.Managers;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class Arena : Observer
    {
        private EnemyManager enemyManager;
        private WaveManager waveManager;
        private BulletManager bulletManager;
        private Shop weaponShop;

        private const int SPAWN_CAP = 5;
        private const float WAVE_FREQUENCY = 150.0F;

        private int score;

        private InputManager inputManager;
        public Player player;
        private const int PLAYER_HEALTH = 20;
        private const float PLAYER_SPEED = 3;
        private const float PLAYER_SPAWN_X = 640;
        private const float PLAYER_SPAWN_Y = 360;

        SpriteFont TNR;
        Vector2 HUDPosition;

        private List<Pickup> pickups;

        public Arena()
        {
            enemyManager = new EnemyManager();
            enemyManager.registerObserver(this);
            waveManager = new WaveManager(SPAWN_CAP, WAVE_FREQUENCY);
            inputManager = new InputManager();
            bulletManager = new BulletManager(enemyManager);
            weaponShop = new Shop(new Vector2(640, 360));

            score = 0;
            HUDPosition = new Vector2(25, 10);
        }

        public void Init()
        {
            player = new Player(PLAYER_HEALTH, PLAYER_SPEED, PLAYER_SPAWN_X, PLAYER_SPAWN_Y);
            player.setBulletmanager(bulletManager);
            pickups = new List<Pickup>();
            AttachListeners();
            Random rand = new Random();
        }

        private void AttachListeners()
        {
            inputManager.Event_MovementPressed += new InputManager.DirectionPressedDelegate(player.UpdateMovement);
            inputManager.Event_SwitchWeapons += new InputManager.ButtonPressedDelegate(player.SwitchWeapons);
            inputManager.Event_UpdateCursorLoc += new InputManager.MousePositionDelegate(player.SetRotation);
            inputManager.Event_Shoot += new InputManager.ButtonPressedDelegate(player.Shoot);
            inputManager.Event_CloseShop += new InputManager.ButtonPressedDelegate(weaponShop.closeShop);
            inputManager.Event_UpgradePistol += new InputManager.ButtonPressedDelegate(player.upgradePistol);
            inputManager.Event_UpgradeShotgun += new InputManager.ButtonPressedDelegate(player.upgradeShotgun);
            inputManager.Event_GiveAmmo += new InputManager.ButtonParamDelegate(player.giveAmmo);
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            SpriteManager.GenerateDefaultTexture(device);

            Texture2D texture = content.Load<Texture2D>("5c2");
            SpriteManager.LoadSprite("enemy", texture);

            texture = content.Load<Texture2D>("Player");
            SpriteManager.LoadSprite("player", texture);

            texture = content.Load<Texture2D>("Pew");
            SpriteManager.LoadSprite("bullet", texture);

            texture = content.Load<Texture2D>("AmmoPickup");
            SpriteManager.LoadSprite("ammo", texture);

            texture = content.Load<Texture2D>("MoneyPickup");
            SpriteManager.LoadSprite("money", texture);

            texture = content.Load<Texture2D>("Shop");
            SpriteManager.LoadSprite("shop", texture);

            TNR = content.Load<SpriteFont>("Times New Roman");

            enemyManager.AddEnemies(waveManager.SpawnWave());
           
            player.LoadSprite("player");
            weaponShop.LoadSprite();
        }

        public void Update(GameTime gameTime)
        {   

            if(enemyManager.GetNumEnemies() == 0)
            {
                enemyManager.AddEnemies(waveManager.SpawnWave());
                weaponShop.openShop();
            }
            else
            {
                if (weaponShop.isOpen())
                {
                    inputManager.shopUpdate(gameTime);
                }
                else
                {
                    inputManager.Update(gameTime);
                    player.Update(gameTime);
                    bulletManager.Update();

                    enemyManager.Update(player.GetPosition());
                    
                    enemyManager.CheckPlayerCollision(player);
                }
            }
            if(player.GetHealth() <= 0 )
            {
                
                player.Shoot();
            }

            HandlePickups();
           
        }

        public void HandlePickups()
        {
            for (int i = pickups.Count - 1; i >= 0; i--)
            {
                //check player collision with
                Pickup pickup = pickups[i];
                float radiiSqr = player.GetRadius() * player.GetRadius() + pickup.GetRadius() * pickup.GetRadius();

                if (radiiSqr > Vector2.DistanceSquared(player.GetPosition(), pickup.GetPosition()))
                {
                    pickup.NotifyObservers();
                    pickups.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            //Draw backgroud

            //batch.Draw(SpriteManager.GetSprite("background"), zero, Color.White);
            foreach (Pickup pickup in pickups)
            {
                pickup.Draw(batch);
            }

            enemyManager.Draw(batch);
            player.Draw(batch);
            bulletManager.Draw(batch);
            weaponShop.Draw(batch);

            string HUDString = "Score: " + score.ToString()
                + "\n" + "Wave: " + waveManager.getWaveNum().ToString()
                + "\n" + "Health: " + player.GetHealth().ToString()
                + "\n" + "Current Weapon: " + player.getWeaponName() + "   Ammo: " + player.getAmmo();
            batch.Begin();
            batch.DrawString(TNR, HUDString, HUDPosition, Color.White);
            batch.End();
        }


        public void GeneratePickup(Vector2 pos)
        {
            //int type = rand.Next(3);

            Pickup pickup;

            AmmoPickup ammoPickup = new AmmoPickup(pos);
            ammoPickup.LoadTexture(SpriteManager.GetSprite("ammo"));
            ammoPickup.registerObserver(player);

            pickup = ammoPickup;

            pickups.Add(pickup);
        }

        public void Notify(ObserverMessages message, int value, Vector2 pos)
        {
            if(message == ObserverMessages.SPAWN_PICKUPS_MESSAGE)
            {
                GeneratePickup(pos);
                score++;
            }

        }
    }
}
