using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public Text firstPlaceText;
    public Text secondPlaceText;
    public Text thirdPlaceText;

    private const string FirstPlaceKey = "FirstPlace";
    private const string SecondPlaceKey = "SecondPlace";
    private const string ThirdPlaceKey = "ThirdPlace";

    private int firstPlaceScore;
    private int secondPlaceScore;
    private int thirdPlaceScore;

    private void Start()
    {
        LoadHighscores();
        UpdateHighscoreText();
    }

    private void LoadHighscores()
    {
        firstPlaceScore = PlayerPrefs.GetInt(FirstPlaceKey, 0);
        secondPlaceScore = PlayerPrefs.GetInt(SecondPlaceKey, 0);
        thirdPlaceScore = PlayerPrefs.GetInt(ThirdPlaceKey, 0);
    }

    private void SaveHighscores()
    {
        PlayerPrefs.SetInt(FirstPlaceKey, firstPlaceScore);
        PlayerPrefs.SetInt(SecondPlaceKey, secondPlaceScore);
        PlayerPrefs.SetInt(ThirdPlaceKey, thirdPlaceScore);
        PlayerPrefs.Save();
    }

    public void UpdateHighscores(int currentScore)
    {
        if (currentScore > firstPlaceScore)
        {
            Debug.Log("erstes if");
            thirdPlaceScore = secondPlaceScore;
            secondPlaceScore = firstPlaceScore;
            firstPlaceScore = currentScore;
            Debug.Log(currentScore);
            Debug.Log(firstPlaceScore);
            Debug.Log(secondPlaceScore);
            Debug.Log(thirdPlaceScore);
        }
        else if (currentScore > secondPlaceScore)
        {
            Debug.Log("zweites if");
            thirdPlaceScore = secondPlaceScore;
            secondPlaceScore = currentScore;
            Debug.Log(currentScore);
            Debug.Log(firstPlaceScore);
            Debug.Log(secondPlaceScore);
            Debug.Log(thirdPlaceScore);
        }
        else if (currentScore > thirdPlaceScore)
        {
            Debug.Log("drittes if");
            thirdPlaceScore = currentScore;
            Debug.Log(currentScore);
            Debug.Log(firstPlaceScore);
            Debug.Log(secondPlaceScore);
            Debug.Log(thirdPlaceScore);
        }

        SaveHighscores();
        UpdateHighscoreText();
    }

    private void UpdateHighscoreText()
    {
        firstPlaceText.text = "" + firstPlaceScore.ToString();
        secondPlaceText.text = "" + secondPlaceScore.ToString();
        thirdPlaceText.text = "" + thirdPlaceScore.ToString();
    }
}