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
        gameOverMessages.Add("You've been out-clucked!");
        gameOverMessages.Add("This coop's too wild for you!");
        gameOverMessages.Add("Chickened out, huh?");
        gameOverMessages.Add("A poultry attempt, indeed!");
        gameOverMessages.Add("Egg-citing, but not quite right!");
        gameOverMessages.Add("You've ruffled some feathers now!");
        gameOverMessages.Add("Looks like you're not the chicken whisperer!");
        gameOverMessages.Add("Too slow, the chickens have flown!");
        gameOverMessages.Add("Brrr-oken dreams! Try again!");
        gameOverMessages.Add("Looks like you're on thin ice!");
        gameOverMessages.Add("Frostbitten and chicken-smitten!");
        gameOverMessages.Add("You just had a snow-down!");
        gameOverMessages.Add("Chilled out too much, huh?");
        gameOverMessages.Add("Ice, ice, baby chicken. Too cold!");
        gameOverMessages.Add("Snow way you just did that!");
        gameOverMessages.Add("Flurried into failure!");
        gameOverMessages.Add("Icy what you did there, but nope!");
        gameOverMessages.Add("Cold feet won't help in this coop!");
        gameOverMessages.Add("Looks like you've hit an ice wall!");
        gameOverMessages.Add("Cool concept, but still game over!");
        gameOverMessages.Add("Fowl weather led to your downfall!");
        gameOverMessages.Add("Game over? You bet your cluck it is!");
        gameOverMessages.Add("Looks like you just winged it!");
        gameOverMessages.Add("This isn't your usual peck-nic!");
        gameOverMessages.Add("Cracked under the pressure, eh?");
        gameOverMessages.Add("Oops, you plucked up!");
        gameOverMessages.Add("Game over! Feather your nest and try again!");
        gameOverMessages.Add("A fowl end to a promising start!");
        gameOverMessages.Add("You hatched a plan, but it didn't work out!");
        gameOverMessages.Add("Your coop, your rules? Nah, I'm the dev. Game over!");
        gameOverMessages.Add("This chicken game's no yolk!");
        gameOverMessages.Add("\"Bawk bawk!\" Translation: You're out!");
        gameOverMessages.Add("Egg-actly what you didn't want to happen!");
        gameOverMessages.Add("You're not good with chicks, aren't you?");
        gameOverMessages.Add("Whoops! Wrong nest!");
        gameOverMessages.Add("You've been out-pecked this time.");
        gameOverMessages.Add("Feathers fly, and so does time.");
        gameOverMessages.Add("Cluck-tastrophe! Better luck next time.");
        gameOverMessages.Add("This game to hard for you?");
        gameOverMessages.Add("Brrr... Too cold for chickens. And for you, it seems!");
        gameOverMessages.Add("Cluck, cluck, game stuck!");
        gameOverMessages.Add("Looks like your chicken sorting skills are on ice!");
        gameOverMessages.Add("Fowl play detected. Restart to try again!");
        gameOverMessages.Add("Feather-brained strategy won't work here.");
        gameOverMessages.Add("Even the chickens are shaking their heads.");
        gameOverMessages.Add("Frostbitten fingers? Slow on the chicken sorting.");
        gameOverMessages.Add("Avalanche of errors!");
        gameOverMessages.Add("The chickens are snowboarding away from your skills.");
        gameOverMessages.Add("These chickens need a hero, not a zero!");
        gameOverMessages.Add("Even the snowmans lasted longer than you.");
        gameOverMessages.Add("These chickens would rather freeze than be sorted by you!");
        gameOverMessages.Add("The chickens have voted you out of the coop.");
        gameOverMessages.Add("You just got served a bucket of icy defeat. Extra crispy!");
        gameOverMessages.Add("Nah, the only thing frozen here is your reaction time.");
        gameOverMessages.Add("Looks like you're the one getting plucked this time.");
        gameOverMessages.Add("Game Over Mr. Chicken Sorter.");
    }
}