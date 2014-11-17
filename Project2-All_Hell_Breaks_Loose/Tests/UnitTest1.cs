using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Project2_All_Hell_Breaks_Loose.Game;
using Project2_All_Hell_Breaks_Loose.Game.Strategies;
using Project2_All_Hell_Breaks_Loose.Game.Weapons;
using Project2_All_Hell_Breaks_Loose.Game.Pickups;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        Player player;

        private void Init()
        {
            player = new Player();
            player.SetPosition(0, 0);

            player.SetHealth(34935);
        }

        [TestMethod]
        public void TestFactory()
        {
            Minion minion = EnemyFactory.makeChaser();

            Assert.AreEqual(EnemyFactory.CHASER_COLOR, minion.GetColor());
        }

        [TestMethod]
        public void TestDecorator()
        {
            AbstractPistol pistol = new Pistol();
            int prevDamage = pistol.GetDamage();

            pistol = new DecoratedPistol(pistol);
            int newDamage = pistol.GetDamage();

            Assert.IsTrue(newDamage > prevDamage);
        }

        [TestMethod]
        public void TestPlayerCollision()
        {
            Init();

            Minion minion = EnemyFactory.makeChaser();
            minion.SetPosition(0, 0);
            minion.SetHeight(345);
            minion.SetWidth(344);

            EnemyManager enemyManager = new EnemyManager();

            enemyManager.AddEnemy(minion);
            
            float prevHealth = player.GetHealth();

            enemyManager.CheckPlayerCollision(player);

            float newHealth = player.GetHealth();

            Assert.IsTrue(prevHealth > newHealth);
        }

        [TestMethod]
        public void TestPlayerShooting()
        {
            BulletManager bulletManager = new BulletManager(null);

            Init();

            player.setBulletmanager(bulletManager);

            player.Shoot();

      
            Assert.IsTrue(bulletManager.GetNumBullets() == 1);
        }

        [TestMethod]
        public void TestPlayerMovement()
        {
            
            Init();

            player.SetSpeed(5);

            Vector2 newPosition = player.GetPosition() + Vector2.One * player.GetSpeed();

            player.UpdateMovement(Vector2.One);

            Assert.IsTrue(player.GetPosition() == newPosition);

        }

        [TestMethod]
        public void TestWaveGeneration()
        {
            int numEnemies = 3;

            WaveManager waves = new WaveManager(numEnemies, 7);

            List<Enemy> enemies = waves.SpawnWave();

            Assert.IsTrue(enemies.Count == numEnemies);
        }

        [TestMethod]
        public void TestAddingEnemies()
        {
            EnemyManager enemyManager = new EnemyManager();

            Enemy minion = new Minion();

            enemyManager.AddEnemy(minion);

            Assert.IsTrue(enemyManager.GetNumEnemies() == 1);
        }

        [TestMethod]
        public void TestKillingEnemy()
        {
            EnemyManager enemyManager = new EnemyManager();

            Enemy minion = EnemyFactory.makeChaser();
            minion.SetHealth(0);

            enemyManager.AddEnemy(minion);

            enemyManager.Update(new Vector2());

            Assert.IsTrue(enemyManager.GetNumEnemies() == 0);
        }

        [TestMethod]
        public void TestSeek()
        {
            Minion minion = EnemyFactory.makeChaser();
            minion.SetPosition(50, 50);

            Vector2 prevPostion = minion.GetPosition();

            minion.Update(Vector2.Zero);

            Vector2 newPosition = minion.GetPosition();

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
            minion.SetPosition(50, 50);
            minion.SetHeight(5);


            Bullet bullet = new Bullet(new Vector2(49, 49), 1, Vector2.One, 3);

            System.Console.WriteLine(bulletManager.GetNumBullets());
            enemyManager.AddEnemy(minion);
            bulletManager.AddBullet(bullet);

            float prevHealth = minion.GetHealth();
            
            bulletManager.Update();
            
            float newHealth = minion.GetHealth();
          
            Assert.IsTrue(bulletManager.GetNumBullets() == 0);
        }
    }
}
