using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2_All_Hell_Breaks_Loose.Game;
using Project2_All_Hell_Breaks_Loose.Game.Weapons;
using Project2_All_Hell_Breaks_Loose;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        Player player;

        private void init()
        {
            player = new Player();
            player.setPosition(0, 0);
            player.setHealth(34935);


        }

        [TestMethod]
        public void TestFactory()
        {
            Minion minion = EnemyFactory.makeChaser();

            Assert.AreEqual(Color.Tomato, minion.getColor());
        }
        [TestMethod]
        public void TestDecorator()
        {
            AbstractPistol pistol = new Pistol();
            int prevDamage = pistol.getDamage();

            pistol = new DecoratedPistol(pistol);
            int newDamage = pistol.getDamage();

            Assert.IsTrue(newDamage > prevDamage);
        }
        [TestMethod]
        public void TestPlayerCollision()
        {
            init();

            Minion minion = EnemyFactory.makeChaser();
            minion.setPosition(0, 0);
            minion.setHeight(345);
            minion.setWidth(344);

            EnemyManager enemyManager = new EnemyManager();

            enemyManager.addEnemy(minion);
            
            float prevHealth = player.getHealth();

            enemyManager.checkPlayerCollision(player);

            float newHealth = player.getHealth();

            Assert.IsTrue(prevHealth > newHealth);
        }

        [TestMethod]
        public void TestPlayerShooting()
        {
            BulletManager bulletManager = new BulletManager(null);

            init();

            player.setBulletmanager(bulletManager);

            player.Shoot();

      
            Assert.IsTrue(bulletManager.getNumBullets() == 1);
        }

        [TestMethod]
        public void TestPlayerMovement()
        {
            
            init();

            player.setSpeed(5);

            Vector2 newPosition = player.getPosition() + Vector2.One * player.getSpeed();

            player.updateMovement(Vector2.One);

            Assert.IsTrue(player.getPosition() == newPosition);

        }

        [TestMethod]
        public void TestWaveGeneration()
        {
            int numEnemies = 3;

            WaveManager waves = new WaveManager(numEnemies, 7);

            List<Enemy> enemies = waves.spawnWave();

            Assert.IsTrue(enemies.Count == numEnemies);
        }

        [TestMethod]
        public void TestAddingEnemies()
        {
            EnemyManager enemyManager = new EnemyManager();

            Enemy minion = new Minion();

            enemyManager.addEnemy(minion);

            Assert.IsTrue(enemyManager.getNumEnemies() == 1);
        }

        [TestMethod]
        public void TestKillingEnemy()
        {
            EnemyManager enemyManager = new EnemyManager();

            Enemy minion = EnemyFactory.makeChaser();
            minion.setHealth(0);

            enemyManager.addEnemy(minion);

            enemyManager.update(new Vector2());

            Assert.IsTrue(enemyManager.getNumEnemies() == 0);
        }

        [TestMethod]
        public void TestSeek()
        {
            Minion minion = EnemyFactory.makeChaser();
            minion.setPosition(50, 50);

            Vector2 prevPostion = minion.getPosition();

            minion.update(Vector2.Zero);

            Vector2 newPosition = minion.getPosition();

            float prevDistance = Vector2.Distance(prevPostion, Vector2.Zero);
            float newDistance = Vector2.Distance(newPosition, Vector2.Zero);

            Assert.IsTrue(newDistance < prevDistance);
        }

        [TestMethod]
        public void TestBullet()
        {
            EnemyManager enemyManager = new EnemyManager();
            BulletManager bulletManager = new BulletManager(enemyManager);

            Minion minion = EnemyFactory.makeChaser();
            minion.setPosition(50, 50);
            minion.setHeight(5);


            Bullet bullet = new Bullet(new Vector2(49, 49), 1, Vector2.One, 3);

            System.Console.WriteLine(bulletManager.getNumBullets());
            enemyManager.addEnemy(minion);
            bulletManager.addBullet(bullet);

            
            

            float prevHealth = minion.getHealth();
            
            bulletManager.Update();
            

            float newHealth = minion.getHealth();
          

            Assert.IsTrue(bulletManager.getNumBullets() == 0);
        }
    }
}
