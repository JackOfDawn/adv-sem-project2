using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Project2_All_Hell_Breaks_Loose.Game;
using Project2_All_Hell_Breaks_Loose.Game.Strategies;
using Project2_All_Hell_Breaks_Loose.Game.Weapons;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies;
using Project2_All_Hell_Breaks_Loose.Game.Managers;
using Project2_All_Hell_Breaks_Loose.Game.Pickups;

namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        Player player;

        private void Init()
        {
            player = new Player();
            player.SetPosition(0, 0);

            player.SetHealth(34935);
        }

        [TestMethod]
        public void TestRandomFactory()
        {
            Minion minion= EnemyFactory.MakeRandomMinion();

            Assert.IsNotNull(minion);
        }

        [TestMethod]
        public void TestChaserMaking()
        {
            Minion minion = EnemyFactory.MakeChaser();

            Assert.AreEqual(EnemyFactory.CHASER_COLOR, minion.GetColor());
        }
        [TestMethod]
        public void TestBlockerMaker()
        {
            Minion minion = EnemyFactory.MakeBlocker();

            Assert.AreEqual(EnemyFactory.BLOCKER_COLOR, minion.GetColor());
        }
        [TestMethod]
        public void TestBansheeMaker()
        {
            Minion minion = EnemyFactory.MakeBanshee();

            Assert.AreEqual(EnemyFactory.BANSHEE_COLOR, minion.GetColor());
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

            Minion minion = EnemyFactory.MakeChaser();
            minion.SetPosition(0, 0);
            minion.SetHeight(345);
            minion.SetWidth(344);

            EnemyManager enemyManager = new EnemyManager();

            enemyManager.AddObject(minion);
            
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



            Assert.IsTrue(bulletManager.GetCount() >= 1);
        }

        [TestMethod]
        public void TestPlayerMovement()
        {
            
            Init();

            player.SetSpeed(5);

            Vector2 newPosition = player.GetPosition() + Vector2.One * player.GetSpeed();

            player.UpdateMovement(Vector2.One);

            Assert.AreEqual(player.GetPosition(), newPosition);

        }

        [TestMethod]
        public void TestWaveGeneration()
        {
            int numEnemies = 4;

            WaveManager waves = new WaveManager(numEnemies, 7);

            List<Enemy> enemies = waves.SpawnWave();

            Assert.AreEqual(enemies.Count, numEnemies);
        }

        [TestMethod]
        public void TestAddingEnemies()
        {
            EnemyManager enemyManager = new EnemyManager();

            Enemy minion = new Minion();

            enemyManager.AddObject(minion);

            Assert.AreEqual(enemyManager.GetCount(), 1);
        }

        [TestMethod]
        public void TestKillingEnemy()
        {
            EnemyManager enemyManager = new EnemyManager();

            Enemy minion = EnemyFactory.MakeChaser();
            minion.SetHealth(0);

            enemyManager.AddObject(minion);

            enemyManager.Update(new Vector2());

            Assert.AreEqual(enemyManager.GetCount(), 0);
        }

        [TestMethod]
        public void TestSeek()
        {
            Minion minion = EnemyFactory.MakeChaser();
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

            Minion minion = EnemyFactory.MakeChaser();
            minion.SetPosition(50, 50);
            minion.SetHeight(5);


            Bullet bullet = new Bullet(new Vector2(49, 49), 1, Vector2.One, 3);

            enemyManager.AddObject(minion);
      		System.Console.WriteLine(bulletManager.GetCount());
            bulletManager.AddObject(bullet);

            float prevHealth = minion.GetHealth();
            
            bulletManager.Update();
            
            float newHealth = minion.GetHealth();

            Assert.AreEqual(bulletManager.GetCount(), 0);
        }

        [TestMethod]
        public void TestEnemyUpgrade()
        {
            Enemy minion = new Minion();

            float prevDamage = minion.GetDamage();
            float prevHealth = minion.GetHealth();

            minion = new EnemyUpgrade(minion);

            float newDamage = minion.GetDamage();
            float newHealth = minion.GetHealth();

            Assert.IsTrue(newDamage > prevDamage);
            Assert.IsTrue(newHealth > prevHealth);
        }

        [TestMethod]
        public void TestShotgun()
        {
            ShotGun shotgun = new ShotGun();
            BulletManager bulletMan = new BulletManager(null);

            shotgun.Shoot(Vector2.Zero, Vector2.Zero, bulletMan);

            Assert.AreEqual(3, bulletMan.GetCount());
        }

        [TestMethod]
        public void TestShotgunUpgrade()
        {
            AbstractShotGun shotgun = new ShotGun();

            float prevDamage = shotgun.GetDamage();

            shotgun = new DecoratedShotgun(shotgun);

            float newDamage = shotgun.GetDamage();

            Assert.IsTrue(newDamage > prevDamage);
        }

        [TestMethod]
        public void GameRestart()
        {
            Arena arena = new Arena();
            arena.Init();

            arena.getPlayer().TakeDamage(5);
            arena.getEnemyManager().AddObject(new Minion());
            arena.restartGame();

            Assert.AreEqual(20, arena.getPlayer().GetHealth());
            Assert.AreEqual(5, arena.getEnemyManager().GetCount());
        }

        [TestMethod]
        public void PlayerWeaponReset()
        {
            Init();

            player.upgradePistol();
            player.upgradeShotgun();

            float prevDamage = player.getCurrentWeapon().GetDamage();

            player.resetWeapons();

            float newDamage = player.getCurrentWeapon().GetDamage();

            Assert.IsTrue(newDamage < prevDamage);
        }

        [TestMethod]
        public void TestSeeking()
        {
            Vector2 startPosition = new Vector2(400, 300);
            Vector2 targetPosition = new Vector2(400, 350);
            int speed = 5;

            MovementStrategy seeking = new SeekMovement();
            Vector2 newPosition = seeking.Update(startPosition, targetPosition, speed);

            Assert.IsTrue(Vector2.DistanceSquared(startPosition, targetPosition) >= Vector2.DistanceSquared(newPosition, targetPosition));

        }

        [TestMethod]
        public void TestFleeing()
        {
            Vector2 startPosition = new Vector2(400, 300);
            Vector2 targetPosition = new Vector2(400, 350);
            int speed = 5;

            MovementStrategy fleeing = new FleeMovement();
            Vector2 newPosition = fleeing.Update(startPosition, targetPosition, speed);

            Assert.IsTrue(Vector2.DistanceSquared(startPosition, targetPosition) <= Vector2.DistanceSquared(newPosition, targetPosition));
        }

    }
}
