using System;
using Level;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    public static bool isGameOver;
    public static string gameOverCause = string.Empty;
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

    /*
     * Possible Game Over Causes:
     * - Timeout
     * - Sorting
     */
    public void GameOver(string cause = "")
    {
        isGameOver = true;
        gameOverCause = cause;

        // stop music
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        
        for (int i = 0; i <  musicObjects.Length; i++)
        {
            musicObjects[i].GetComponent<AudioSource>().Stop();
        }

        // play game over sound
        transform.GetChild(0).GetComponent<AudioSource>().Play();
    }
}