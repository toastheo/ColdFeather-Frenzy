using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject ingameScore;
    public GameObject PausePanel;
    public GameObject OptionsPanel;
    public static bool isPaused;
    private HighscoreManager highscoreManager;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        // get HighscoreManager reference
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager").GetComponent<HighscoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        ingameScore.SetActive(false);
        PausePanel.SetActive(true);
        OptionsPanel.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ContinueGame()
    {
        ingameScore.SetActive(true);
        pauseMenu.SetActive(false);
        PausePanel.SetActive(false);
        OptionsPanel.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Debug.Log("pausemainmenu");
        // Call the UpdateHighscores method before leaving to menu
        highscoreManager.UpdateHighscores(GameLogicScript.score);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        isPaused = false;
    }
}
