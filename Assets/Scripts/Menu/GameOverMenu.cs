using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameoverMenu;
    public GameObject ingameScore;
    // Start is called before the first frame update
    void Start()
    {
        gameoverMenu.SetActive(false);
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
        GameLogicScript.score = 0;
        Time.timeScale = 1f;
        GameLogicScript.isGameOver = false;
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }


    public void GameOverDisplay()
    {
        ingameScore.SetActive(false);
        gameoverMenu.SetActive(true);
        Time.timeScale = 0f;
        
    }
}
