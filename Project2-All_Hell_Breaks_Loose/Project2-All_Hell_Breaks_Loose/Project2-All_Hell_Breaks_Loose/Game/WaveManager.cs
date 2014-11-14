using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    class WaveManager
    {
        //the highest number of enemies that can be spawned in one wave
        private int spawnCap;
        //time between wave spawns
        private float waveFrequency;
        private float countdownTillNextWave;

        public WaveManager(int spawnCap, float waveFrequency)
        {
            this.spawnCap = spawnCap;
            this.waveFrequency = waveFrequency;
            countdownTillNextWave = waveFrequency;
        }

        public List<Enemy> update()
        {
            if (countdownTillNextWave <= 0)
            {
                countdownTillNextWave = waveFrequency;
                return spawnWave();
            }
            else
            {
                countdownTillNextWave--;
                return null;
            }
        }

        public List<Enemy> spawnWave()
        {
            List<Enemy> wave = new List<Enemy>();

            Minion minion = EnemyFactory.makeMinion();
            minion.setPosition(-50.0f, 50.0f);

            wave.Add(minion);

            return wave;
        }

        public void setSpawnCap(int newSpawnCap)
        {
            spawnCap = newSpawnCap;
        }
        public int getSpawnCap()
        {
            return spawnCap;
        }
        public void setWaveFrequency(float newWaveFrequency)
        {
            waveFrequency = newWaveFrequency;
        }
        public float getWaveFrequency()
        {
            return waveFrequency;
        }
    }
}
