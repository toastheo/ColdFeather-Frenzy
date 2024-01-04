using System;
using System.IO;
using UnityEngine;

namespace Level
{
    public class LevelDataManager : MonoBehaviour
    {
        public static LevelDataManager Instance { get; private set; } // Singleton Instance

        public int CurrentLevel { get; private set; } // Encapsulate current level
        public float SpawningInterval { get; private set; }
        public float LifeTimeChicken { get; private set; }

        [SerializeField] private LevelData[] levels;

        private TimeSpan lastUpdateTime;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadLevels();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadLevels()
        {
            string levelPath = Path.Combine(Application.dataPath, "Level/levels.json");
            string levelString = File.ReadAllText(levelPath);
            levels = JsonUtility.FromJson<LevelDataWrapper>("{\"levels\":" + levelString + "}").levels;
        }

        private void Start()
        {
            // load the first level
            UpdateLevelData(1);
            lastUpdateTime = WorldTime.WorldTime.currentTime;
        }

        private void Update()
        {
            TimeSpan currentTime = WorldTime.WorldTime.currentTime;
            if (currentTime.Days != lastUpdateTime.Days)
            {
                UpdateLevelData(currentTime.Days);
                lastUpdateTime = currentTime;
            }
            
            //print("curr Time Day: "+currentTime.Days + " last Upd: " + lastUpdateTime.Days);
        }

        private void UpdateLevelData(int day)
        {
            // Adjust for day one
            int index = day - 1;
            
            if (index >= 0 && index < levels.Length )
            {
                SpawningInterval = levels[index].spawningInterval;
                CurrentLevel = levels[index].level;
                LifeTimeChicken = levels[index].lifeTimeChicken;
            }
            else if (index >= levels.Length)
            {
                // Fallback to last level if reached max level
                SpawningInterval = levels[levels.Length - 1].spawningInterval;
                CurrentLevel = levels[levels.Length - 1].level;
                LifeTimeChicken = levels[levels.Length - 1].lifeTimeChicken;
            }
        }

        [Serializable]
        private class LevelDataWrapper
        {
            public LevelData[] levels;
        }
    }
}