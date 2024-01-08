using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
     public void PlayGame()
     {
        StartCoroutine(PlayGameAfterAudio());
     }

    private IEnumerator PlayGameAfterAudio()
    {
        // deactivate play button
        Button playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();
        playButton.enabled = false;

        // update Playing state
        GameManager.Instance.ChangeGameState(GameState.StartPlaying);
        
        AudioSource startSound = GetComponent<AudioSource>();
        startSound.Play();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        asyncLoad.allowSceneActivation = false;

        yield return new WaitWhile(() => startSound.isPlaying);

        yield return new WaitUntil(() => asyncLoad.progress >= 0.9f);

        GameLogicScript.score = 0;
        GameLogicScript.isGameOver = false;

        asyncLoad.allowSceneActivation = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
