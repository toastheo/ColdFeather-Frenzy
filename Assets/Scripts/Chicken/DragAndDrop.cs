using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool canMove = false;
    private bool dragging = false;
    public bool Dragging => dragging;

    // for checking if mouse is over game object
    private Collider2D mouseOverObject;

    // define frame
    [SerializeField] private Vector2 maxBound = new(8, 4);
    [SerializeField] private Vector2 minBound = new(-8, -4);

    private ChickenBehaviourScript chickenBehaviourScript;
    private GameLogicScript gameLogicScript;

    // for chicken noices
    private AudioSource audioSource;
    [SerializeField] private float noisesFadeOutTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // get Collider
        mouseOverObject = GetComponent<Collider2D>();

        // get Chicken Behaviour Script
        chickenBehaviourScript = GetComponent<ChickenBehaviourScript>();

        // get Game Logic Script
        gameLogicScript = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogicScript>();

        // get audiosource
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator FadeOutCoroutine(float fadeOutTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;   // not necessary but just in case
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            // 0 = left Mouse BTN
            // 1 = righte Mouse BTN
            // 2 = middle Mouse BTN
            {
                canMove = mouseOverObject == Physics2D.OverlapPoint(mousePos);

                if (canMove)
                {
                    dragging = true;
                    audioSource.Play();
                }
            }

            if (dragging)
            {
                // clamp and store mousePos
                Vector2 clampPos = new(
                    Mathf.Clamp(mousePos.x, minBound.x, maxBound.x),
                    Mathf.Clamp(mousePos.y, minBound.y, maxBound.y)
                );

                transform.position = clampPos;
            }

            if (Input.GetMouseButtonUp(0) && dragging)
            {
                canMove = false;
                dragging = false;
                StartCoroutine(FadeOutCoroutine(noisesFadeOutTime));

                // check if chicken was caught
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, chickenBehaviourScript.StopLayer);
                if (hit.collider != null)
                {
                    chickenBehaviourScript.wasCaught = true;

                    if (chickenBehaviourScript.ChickenStableMatch())
                    {
                        GameLogicScript.score += 1;
                    }
                    // check if chicken matches the stable
                    if (!chickenBehaviourScript.ChickenStableMatch())
                        gameLogicScript.GameOver("Chicken was dragged into the wrong stable.");

                    // disable script
                    enabled = false;
                }
            }
        }
        
    }
}
