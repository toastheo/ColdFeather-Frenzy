using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public void PlayGame()
     {
        StartCoroutine(PlayGameAfterAudio());
     }

    private IEnumerator PlayGameAfterAudio()
    {
        AudioSource startSound = GetComponent<AudioSource>();
        startSound.Play();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        asyncLoad.allowSceneActivation = false;

        yield return new WaitWhile(() => startSound.isPlaying);

        yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);

        GameLogicScript.score = 0;
        Time.timeScale = 1f;
        GameLogicScript.isGameOver = false;

        asyncLoad.allowSceneActivation = true;
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
