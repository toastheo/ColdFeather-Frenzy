using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAmbienteScript : MonoBehaviour
{
    private GameLogicScript gameLogicScript;
    private float timer = 0;
    private AudioSource backgroundAudio;

    // Start is called before the first frame update
    void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
        gameLogicScript = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > gameLogicScript.SpawningInterval)
        {
            backgroundAudio.Play();
            enabled = false;
        }
    }
}
