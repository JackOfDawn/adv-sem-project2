﻿using System;
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
        private DeathScreen deathScreen;

        private const int MINIMUM_SPAWN = 5;

        private int score;
        private int previousScore;
        private int highScore;
        private int DEFAULT_HIGH_SCORE = 20;

        private InputManager inputManager;
        public Player player;
        private const int PLAYER_HEALTH = 20;
        private const float PLAYER_SPEED = 3;

        private const float CENTER_X = 640;
        private const float CENTER_Y = 360;
        private Vector2 CENTER_POINT = new Vector2(CENTER_X, CENTER_Y);

        HUD hud;
        
        private Random rand;
        private List<Pickup> pickups;


        public Arena()
        {
            enemyManager = new EnemyManager();
            enemyManager.registerObserver(this);
            waveManager = new WaveManager(MINIMUM_SPAWN);
            inputManager = new InputManager();
            bulletManager = new BulletManager(enemyManager);
            rand = new Random();
            weaponShop = new Shop(CENTER_POINT);
            deathScreen = new DeathScreen(CENTER_POINT);
            
            previousScore = score = 0;
            highScore = DEFAULT_HIGH_SCORE;

            hud = new HUD(new Vector2(25, 10));
        }

        public void Init()
        {
           
            player = new Player(PLAYER_HEALTH, PLAYER_SPEED, CENTER_POINT);
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
            inputManager.Event_Respawn += new InputManager.ButtonPressedDelegate(restartGame);
        }

        public void restartGame()
        {

            previousScore = score;

            if (previousScore > highScore)
                highScore = previousScore;

            player.SetHealth(PLAYER_HEALTH);
            player.SetPosition(CENTER_POINT);
            player.resetWeapons();
            deathScreen.setDraw(false);
            waveManager.setWaveNum(0);
            enemyManager.DeleteAll();
            bulletManager.DeleteAll();
            enemyManager.Update(player.GetPosition());
            pickups.Clear();
            
            score = 0;

            enemyManager.AddObjects(waveManager.SpawnWave());
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

            texture = content.Load<Texture2D>("You-Died");
            SpriteManager.LoadSprite("death", texture);

            SpriteFont TNR = content.Load<SpriteFont>("Times New Roman");
            hud.loadFont(TNR);

            enemyManager.AddObjects(waveManager.SpawnWave());
           
            player.LoadSprite("player");
            weaponShop.LoadSprite("shop");
            deathScreen.LoadSprite("death");
        }

        public void Update(GameTime gameTime)
        {   

            if(enemyManager.GetCount() == 0)
            {
                enemyManager.AddObjects(waveManager.SpawnWave());
                weaponShop.openShop();
            }
            else if (player.GetHealth() <= 0)
            {
                deathScreen.setDraw(true);
                inputManager.deathUpdate(gameTime);
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
     

            HandlePickups();

            hud.update(score, highScore, previousScore, waveManager.getWaveNum(), player.GetHealth(), player.getWeaponName(), player.getCurrentWeapon().getAmmo());
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
            foreach (Pickup pickup in pickups)
            {
                pickup.Draw(batch);
            }

            enemyManager.Draw(batch);
            player.Draw(batch);
            bulletManager.Draw(batch);
            weaponShop.Draw(batch);
            deathScreen.Draw(batch);

            hud.draw(batch);
        }

        public Player getPlayer()
        {
            return player;
        }

        public BulletManager getBulletManager()
        {
            return bulletManager;
        }

        public EnemyManager getEnemyManager()
        {
            return enemyManager;
        }

        public void GeneratePickup(Vector2 pos)
        {
            int type = rand.Next(3);
            if (type == 1)
            {
                Pickup pickup;

                AmmoPickup ammoPickup = new AmmoPickup(pos);
                ammoPickup.LoadSprite();
                ammoPickup.registerObserver(player);

                pickup = ammoPickup;

                pickups.Add(pickup);
            }
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
