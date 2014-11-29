using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies;

namespace Project2_All_Hell_Breaks_Loose.Game.Managers
{
    public class WaveManager
    {
        //the highest number of enemies that can be spawned in one wave
        private int spawnCap;
        //time between wave spawns
        private float waveFrequency;
        private float countdownTillNextWave;
        private int waveNumber;

        public WaveManager(int spawnCap, float waveFrequency)
        {
            this.spawnCap = spawnCap;
            this.waveFrequency = waveFrequency;
            countdownTillNextWave = waveFrequency;
            waveNumber = 0;
            
        }

        public List<Enemy> Update()
        {
            if (countdownTillNextWave <= 0)
            {
                countdownTillNextWave = waveFrequency;
                return SpawnWave();
            }
            else
            {
                countdownTillNextWave--;
                return null;
            }
        }

        public List<Enemy> SpawnWave()
        {
            List<Enemy> wave = new List<Enemy>();


            for (int i = 0; i < spawnCap + waveNumber /2; i++)
            {
                
                Enemy minion = EnemyFactory.MakeRandomMinion();
                minion.SetPosition(0.0f, 32.0f * (i*2));

                for (int j = 0; j < waveNumber; j ++)
                {
                    minion = new EnemyUpgrade(minion);
                }


                wave.Add(minion); 
            }

            waveNumber++;
            return wave;
        }

        public void SetSpawnCap(int newSpawnCap)
        {
            spawnCap = newSpawnCap;
        }
        public int GetSpawnCap()
        {
            return spawnCap;
        }
        public void SetWaveFrequency(float newWaveFrequency)
        {
            waveFrequency = newWaveFrequency;
        }
        public float GetWaveFrequency()
        {
            return waveFrequency;
        }
        public int getWaveNum()
        {
            return waveNumber;
        }
    }
}
