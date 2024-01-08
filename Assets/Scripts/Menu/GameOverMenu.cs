using Chicken;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameoverMenu;
    public GameObject ingameScore;
    public GameObject PauseButton;
    public TextMeshProUGUI gameOverText;

    private HighscoreManager highscoreManager;
    private List<string> gameOverMessages = new List<string>();
    private string selectedGameOverText;
    private bool messageGenerated = false;

    private void Start()
    {
        gameoverMenu.SetActive(false);
        highscoreManager = GameObject.FindGameObjectWithTag("HighscoreManager")?.GetComponent<HighscoreManager>();
        AddMessages();
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
        if (!messageGenerated)
            GenerateGameOverMessage();

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

    private void GenerateGameOverMessage()
    {
        if (gameOverMessages.Count > 0)
        {
            int randomIndex = Random.Range(0, gameOverMessages.Count);
            selectedGameOverText = gameOverMessages[randomIndex];
        } 
        else
        {
            Debug.LogError("GameOverText list is empty!");
        }

        gameOverText.text = selectedGameOverText + "\n\n";

        switch (GameLogicScript.gameOverCause)
        {
            case "Timeout":
                gameOverText.text += "You let a chicken die because it was in the cold for too long.";
                break;
            case "Sorting":
                gameOverText.text += "You let a chicken die because you sorted it into the wrong enclosure.";
                break;
            default:
                gameOverText.text += "You let a chicken die.";
                break;
        }

        messageGenerated = true;
    }

    private void AddMessages()
    {
        gameOverMessages.Add("Cluck! You scrambled the mission.");
        gameOverMessages.Add("Egg-stra time needed next time!");
        gameOverMessages.Add("Feather luck next time!");
        gameOverMessages.Add("You just got pecked out of the game!");
        gameOverMessages.Add("Looks like you flew the coop too soon!");
        gameOverMessages.Add("The chickens have flown the coop!");
        gameOverMessages.Add("Egg-sasperating! Try again!");
        gameOverMessages.Add("Not egg-zactly the right move!");
        gameOverMessages.Add("You've been out-clucked!");
        gameOverMessages.Add("Fowl play detected!");
        gameOverMessages.Add("This coop's too wild for you!");
        gameOverMessages.Add("Chickened out, huh?");
        gameOverMessages.Add("Egg-ceptionally close, but nope!");
        gameOverMessages.Add("Looks like your goose is cooked!");
        gameOverMessages.Add("A poultry attempt, indeed!");
        gameOverMessages.Add("Egg-citing, but not quite right!");
        gameOverMessages.Add("You've ruffled some feathers now!");
        gameOverMessages.Add("The eggs have cracked under pressure!");
        gameOverMessages.Add("Looks like you're not the chicken whisperer!");
        gameOverMessages.Add("Too slow, the chickens have flown!");
        gameOverMessages.Add("Brrr-oken dreams! Try again!");
        gameOverMessages.Add("Looks like you're on thin ice!");
        gameOverMessages.Add("Frostbitten and chicken-smitten!");
        gameOverMessages.Add("You just had a snow-down!");
        gameOverMessages.Add("Chilled out too much, huh?");
        gameOverMessages.Add("Ice, ice, baby chicken. Too cold!");
        gameOverMessages.Add("Frozen in your tracks!");
        gameOverMessages.Add("Slippery when cold! Game over!");
        gameOverMessages.Add("Winter is not coming, because you're out!");
        gameOverMessages.Add("Snow way you just did that!");
        gameOverMessages.Add("Avalanched by mistakes!");
        gameOverMessages.Add("Flurried into failure!");
        gameOverMessages.Add("Icy what you did there, but nope!");
        gameOverMessages.Add("Cold feet won't help in this coop!");
        gameOverMessages.Add("Looks like you've hit an ice wall!");
        gameOverMessages.Add("Chickens turned into ice cubes!");
        gameOverMessages.Add("Egg-cicles aren't the goal!");
        gameOverMessages.Add("Cool concept, but still game over!");
        gameOverMessages.Add("Fowl weather led to your downfall!");
    }
}