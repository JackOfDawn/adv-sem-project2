using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project2_All_Hell_Breaks_Loose.Game.GameObjects.Enemies;

namespace Project2_All_Hell_Breaks_Loose.Game.Managers
{
    public class WaveManager
    {
        //the lowest number of enemies that will spawn
        private int baseEnemyCount;
        private int waveNumber;

        public WaveManager(int spawnCap)
        {
            this.baseEnemyCount = spawnCap;
            waveNumber = 0;
            
        }

        public List<Enemy> SpawnWave()
        {
            List<Enemy> wave = new List<Enemy>();


            for (int i = 0; i < baseEnemyCount + waveNumber; i++)
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
            baseEnemyCount = newSpawnCap;
        }
        public int GetSpawnCap()
        {
            return baseEnemyCount;
        }
        public void setWaveNum(int wave)
        {
            waveNumber = wave;
        }
        public int getWaveNum()
        {
            return waveNumber;
        }
    }
}
