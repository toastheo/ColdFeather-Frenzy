using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Level
{
    public class LevelDataManager : MonoBehaviour
    {
        private int levelNumber = 0;
        private TimeSpan currentTime;
        
        public int level;
        public float spawningInterval;
        public float lifeTimeChicken;
        public LevelData[] levels;
        
        public static LevelDataManager Instance { get; private set; } // Singleton Instance 
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            string levelPath = Path.Combine(Application.dataPath, "Level/levels.json");
            string levelString = File.ReadAllText(levelPath);
    
            levels = JsonUtility.FromJson<LevelDataWrapper>("{\"levels\":" + levelString + "}").levels;
        }

        private void Update()
        {
            currentTime = WorldTime.WorldTime.currentTime; // get current Time from World Time Script

          if (currentTime.Days == levelNumber)
          {
              if (levelNumber < levels.Length)
              {
                  spawningInterval = levels[levelNumber].spawningInterval; 
                  level = levels[levelNumber].level;
                  lifeTimeChicken = levels[levelNumber].lifeTimeChicken;
              }

              levelNumber++;
          }
          else if (levelNumber >= levels.Length)
          {
              spawningInterval = levels[levels.Length - 1].spawningInterval;
              level = levels[levels.Length - 1].level;
              lifeTimeChicken = levels[levels.Length - 1].lifeTimeChicken;
          }

        }

        [Serializable]
        private class LevelDataWrapper
        {
            public LevelData[] levels;
        }

    }
}
