using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WorldTime;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject ingameScore;
    public GameObject PauseButton;
    public GameObject PausePanel;
    public GameObject OptionsPanel;
    public static bool isPaused;
    
    private HighscoreManager highscoreManager;
    private WorldTime.WorldTime worldTime;

    // Start is called before the first frame update
    private void Start()
    {
        pauseMenu.SetActive(false);
        // get HighscoreManager reference
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager").GetComponent<HighscoreManager>();
        
        // Initialize worldTime
        worldTime = WorldTime.WorldTime.Instance;

    }

    // Update is called once per frame
    private void Update()
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
        PauseButton.SetActive(false);
        ingameScore.SetActive(false);
        PausePanel.SetActive(true);
        OptionsPanel.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.Paused);
    }

    public void ContinueGame()
    {
        PauseButton.SetActive(true);
        Debug.Log("Continue Game");
        ingameScore.SetActive(true);
        pauseMenu.SetActive(false);
        PausePanel.SetActive(false);
        OptionsPanel.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        
        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.ContinuePlaying);
    }

    public void GoToMainMenu()
    {
        // Call the UpdateHighscores method before leaving to menu
        highscoreManager.UpdateHighscores(GameLogicScript.score);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        isPaused = false;
        
        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
