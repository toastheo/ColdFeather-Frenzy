using System;
using Level;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    public static bool isGameOver;
    public static int score;

    /// <summary>
    /// Read only the given Property from the LevelDataManager.Instance if its not null
    /// If its null or can't be accessed, return default value 0.0f or 0
    /// </summary>
    public float SpawningInterval => LevelDataManager.Instance?.SpawningInterval ?? default;
    public float LifeTimeChicken => LevelDataManager.Instance?.LifeTimeChicken ?? default;
    public int currentLevel => LevelDataManager.Instance?.CurrentLevel ?? default;
    
    [SerializeField] public float flashAmount;
    [SerializeField] public float closeToDying;

    
    // public methods
    public void GameOver(string customMessage = "No custom message was provided.")
    {
        // implement game over animation & screen right here

        // PLACEHOLDER
        //Debug.Log($"Game Over: {customMessage}");
        isGameOver = true;
    }
    
    /*
    private void Update()
    {
        print("spawning: " + SpawningInterval + 
              " lifeTime: " + LifeTimeChicken + " level: " +currentLevel);
    }
    */
}