using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            GameLogicScript.highscore = PlayerPrefs.GetInt("Highscore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLogicScript.score > GameLogicScript.highscore)
        {
            GameLogicScript.highscore = GameLogicScript.score;
            PlayerPrefs.SetInt("Highscore", GameLogicScript.highscore);
        }

        highscoreText.text = "" + GameLogicScript.highscore;
    }
}
