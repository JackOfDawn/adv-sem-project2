using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class Arena
    {
        private EnemyManager enemyManager;
        private WaveManager waveManager;
        private const int SPAWN_CAP = 2;
        private const float WAVE_FREQUENCY = 150.0F;

        private InputManager inputManager;
        private Player player;
        private const int PLAYER_HEALTH = 200;
        private const float PLAYER_SPEED = 3;
        private const float PLAYER_SPAWN_X = 640;
        private const float PLAYER_SPAWN_Y = 360;

        public Arena()
        {
            enemyManager = new EnemyManager();
            waveManager = new WaveManager(SPAWN_CAP, WAVE_FREQUENCY);
            inputManager = new InputManager();
        }

        public void init()
        {
            player = new Player(PLAYER_HEALTH, PLAYER_SPEED, PLAYER_SPAWN_X, PLAYER_SPAWN_Y);
            attachListeners();
        }

        private void attachListeners()
        {
            inputManager.event_MovementPressed += new InputManager.DirectionPressedDelegate(player.updateMovement);
            inputManager.event_SwitchWeapons += new InputManager.ButtonPressedDelegate(player.switchWeapons);
            inputManager.event_UpdateCursorLoc += new InputManager.MousePositionDelegate(player.setRotation);
        }

        public void loadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("5c2");
            SpriteManager.loadSprite("enemy", texture);

            texture = content.Load<Texture2D>("ship");
            SpriteManager.loadSprite("player", texture);

            player.loadSprite();
        }

        public void update(GameTime gameTime)
        {
            inputManager.update(gameTime);
            player.update();

            if(enemyManager.getNumEnemies() == 0)
            {
                enemyManager.addEnemies(waveManager.spawnWave());
            }
            else
            {
                enemyManager.update(player.getPosition());
            }

        }
        public void draw(SpriteBatch batch)
        {
            enemyManager.draw(batch);
            player.draw(batch);
        }
    }
}
