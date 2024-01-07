using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameoverMenu;
    public GameObject ingameScore;

    private HighscoreManager highscoreManager;
    
    private void Start()
    {
        gameoverMenu.SetActive(false);
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager")?.GetComponent<HighscoreManager>();

    }

    private void Update()
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
        GameLogicScript.isGameOver = false;
        SceneManager.LoadSceneAsync(1);
        
        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.StartPlaying);
    }

    public void OpenMainMenu()
    {
        Debug.Log("gomainmenu");
        highscoreManager?.UpdateHighscores(GameLogicScript.score);
        SceneManager.LoadSceneAsync(0);
        
        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }

    private void GameOverDisplay()
    {
        ingameScore.SetActive(false);
        gameoverMenu.SetActive(true);

        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.GameOver);
    }
}