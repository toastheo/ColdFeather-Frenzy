using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameoverMenu;
    public GameObject ingameScore;
    public GameObject PauseButton;

    private HighscoreManager highscoreManager;

    // Start is called before the first frame update
    void Start()
    {
        gameoverMenu.SetActive(false);
        // get HighscoreManager reference
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager").GetComponent<HighscoreManager>();
    }

    // Update is called once per frame
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
        // Call the UpdateHighscores method when gameOver is true
        highscoreManager.UpdateHighscores(GameLogicScript.score);
        GameLogicScript.score = 0;
        Time.timeScale = 1f;
        GameLogicScript.isGameOver = false;
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenMainMenu()
    {
        Debug.Log("gomainmenu");
        // Call the UpdateHighscores method when gameOver is true
        highscoreManager.UpdateHighscores(GameLogicScript.score);
        SceneManager.LoadSceneAsync(0);
    }


    public void GameOverDisplay()
    {
        PauseButton.SetActive(false);
        ingameScore.SetActive(false);
        gameoverMenu.SetActive(true);
        Time.timeScale = 0f; 
    }
}
