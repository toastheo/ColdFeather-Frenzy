using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject ingameScore;
    public static bool isPaused;
    private AudioSource chickenAmbiente;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        chickenAmbiente = GameObject.FindGameObjectWithTag("ChickenAmbiente").GetComponent<AudioSource>();
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        chickenAmbiente.Pause();
    }

    public void ContinueGame()
    {
        ingameScore.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        chickenAmbiente.UnPause();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        isPaused = false;
    }
}
