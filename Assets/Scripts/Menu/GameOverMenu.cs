using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WorldTime;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameoverMenu;
    public GameObject ingameScore;

    private HighscoreManager highscoreManager;
    private WorldTime.WorldTime worldTime;

    void Start()
    {
        gameoverMenu.SetActive(false);
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager")?.GetComponent<HighscoreManager>();
        worldTime = FindObjectOfType<WorldTime.WorldTime>();
    }

    void Update()
    {
        if(GameLogicScript.isGameOver)
        {
            GameOverDisplay();
        }
    }

    public void RestartGame()
    {
        Debug.Log("gorestartgame");
        highscoreManager?.UpdateHighscores(GameLogicScript.score);
        GameLogicScript.score = 0;
        Time.timeScale = 1f;
        GameLogicScript.isGameOver = false;
        SceneManager.LoadSceneAsync(1);

        // Restart World Time
        if (worldTime != null) 
        {
            worldTime.StartTime();
        }
    }

    public void OpenMainMenu()
    {
        Debug.Log("gomainmenu");
        highscoreManager?.UpdateHighscores(GameLogicScript.score);
        SceneManager.LoadSceneAsync(0);
    }

    private void GameOverDisplay()
    {
        ingameScore.SetActive(false);
        gameoverMenu.SetActive(true);
        Time.timeScale = 0f;

        // Stop and Reset World Time
        if (worldTime != null)
        {
            worldTime.StopTime();
            worldTime.ResetTime();
            //print(worldTime);
        }
    }
}