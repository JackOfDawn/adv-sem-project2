using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public class Arena
    {
        private EnemyManager enemyManager;
        private WaveManager waveManager;
        private BulletManager bulletManager;

        private const int SPAWN_CAP = 5;
        private const float WAVE_FREQUENCY = 150.0F;

        private InputManager inputManager;
        public Player player;
        private const int PLAYER_HEALTH = 200;
        private const float PLAYER_SPEED = 3;
        private const float PLAYER_SPAWN_X = 640;
        private const float PLAYER_SPAWN_Y = 360;

        public Arena()
        {
            enemyManager = new EnemyManager();
            waveManager = new WaveManager(SPAWN_CAP, WAVE_FREQUENCY);
            inputManager = new InputManager();
            bulletManager = new BulletManager(enemyManager);
        }

        public void init()
        {
            player = new Player(PLAYER_HEALTH, PLAYER_SPEED, PLAYER_SPAWN_X, PLAYER_SPAWN_Y);
            player.setBulletmanager(bulletManager);
            attachListeners();
            
        }

        private void attachListeners()
        {
            inputManager.event_MovementPressed += new InputManager.DirectionPressedDelegate(player.updateMovement);
            inputManager.event_SwitchWeapons += new InputManager.ButtonPressedDelegate(player.switchWeapons);
            inputManager.event_UpdateCursorLoc += new InputManager.MousePositionDelegate(player.setRotation);
            inputManager.event_Shoot += new InputManager.ButtonPressedDelegate(player.Shoot);
        }

        public void loadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("5c2");
            SpriteManager.loadSprite("enemy", texture);

            texture = content.Load<Texture2D>("Player");
            SpriteManager.loadSprite("player", texture);

            texture = content.Load<Texture2D>("Pew");
            SpriteManager.loadSprite("bullet", texture);

            texture = content.Load<Texture2D>("spce");
            SpriteManager.loadSprite("background", texture);

            player.loadSprite();
          
        }

        public void update(GameTime gameTime)
        {
            inputManager.update(gameTime);
            player.update(gameTime);
            bulletManager.Update(gameTime);

            if(enemyManager.getNumEnemies() == 0)
            {
                enemyManager.addEnemies(waveManager.spawnWave());
            }
            else
            {
                enemyManager.update(player.getPosition());
                enemyManager.checkPlayerCollision(player);
            }
            if(player.getHealth() <= 0 )
            {
                player.Shoot();
            }
        }
        public void draw(SpriteBatch batch)
        {
            //draw backgroud
            batch.Begin();
            //batch.Draw(SpriteManager.getSprite("background"), zero, Color.White);
            batch.End();

            enemyManager.draw(batch);
            player.draw(batch);
            bulletManager.Draw(batch);
        }
    }
}
