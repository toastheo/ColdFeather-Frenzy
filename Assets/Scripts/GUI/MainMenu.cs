using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public void PlayGame()
     {
        Time.timeScale = 1f;
        GameLogicScript.isGameOver = false;
        SceneManager.LoadSceneAsync(1);
     }

    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
