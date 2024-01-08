using Chicken;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameoverMenu;
    public GameObject ingameScore;
    public GameObject PauseButton;

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

        // destroy global light before restarting
        GameObject globalLight = GameObject.FindGameObjectWithTag("GlobalLight");
        Destroy(globalLight);

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
        PauseButton.SetActive(false);
        ingameScore.SetActive(false);
        gameoverMenu.SetActive(true);

        // stop all chicken drag and drops
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");
        for (int i = 0; i < chickens.Length; i++)
        {
            DragAndDrop dragAndDropScript = chickens[i].GetComponent<DragAndDrop>();
            dragAndDropScript.GetComponent<AudioSource>().Stop();
            dragAndDropScript.enabled = false;
        }

        // Update Game State
        GameManager.Instance.ChangeGameState(GameState.GameOver);
    }
}