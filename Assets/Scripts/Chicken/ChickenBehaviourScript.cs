using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ChickenBehaviourScript : MonoBehaviour
{
    private enum ChickenColor
    {
        Red,
        Blue
    }

    private bool isWalking = false;
    public bool IsWalking => isWalking;

    private GameObject target;
    private bool hasBeenOnScreen = false;
    private Vector2 screenBounds;
    private GameLogicScript gameLogicScript;
    private float timer = 0;
    private Animator animator;
    private DragAndDrop dragAndDropScript;
    private DamagFlash damagFlash;
    
    public bool wasCaught = false;
    private float lifetime;
    private float closeToDying;
    private float flashAmount;
    private float firstDieAni;

    [SerializeField] private ChickenColor color = ChickenColor.Red;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask stopLayer;
    public LayerMask StopLayer => stopLayer;

    [Header("Time parameters")]
    [SerializeField] private float maxWalkTime = 2f;
    [SerializeField] private float minWalkTime = 1f;
    [SerializeField] private float maxStopTime = 1f;
    [SerializeField] private float minStopTime = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        // find game logic script
        gameLogicScript = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogicScript>();

        // get life time of the chickens
        lifetime = gameLogicScript.lifeTimeChicken;

        // find target
        target = GameObject.FindWithTag("TargetPoint");

        // rotate chicken in direction of the target
        RotateToTarget();

        // check if time parameters are correctly applied
        if (minWalkTime > maxWalkTime)
            minWalkTime = maxWalkTime;
        if (minStopTime > maxStopTime)
            minStopTime = maxStopTime;

        // get screen bounds from main camera
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // get animator
        animator = transform.GetChild(0).GetComponent<Animator>();

        // get drag and drop script
        dragAndDropScript = GetComponent<DragAndDrop>();
        
        // get damage flash script
        damagFlash = GetComponent<DamagFlash>();
        
        // get then close to dying starts
        closeToDying = gameLogicScript.closeToDying;
        
        // set how many times the closeToDying is showing
        flashAmount  = gameLogicScript.flashAmount;
        firstDieAni = lifetime * closeToDying;
        
        
        
        StartCoroutine(WalkAndStop());
    }

    // Coroutine to let the chicken walk and stop between random parameters
    private IEnumerator WalkAndStop()
    {
        while (true)
        {
            isWalking = !isWalking;

            float time;
            if (isWalking)
            {
                // rotate chicken in direction of the target or in a random direction if it was caught
                if (!wasCaught)
                    RotateToTarget();
                else
                    RotateRandomly();

                time = Random.Range(minWalkTime, maxWalkTime);
            }
            else
            {
                time = Random.Range(minStopTime, maxStopTime);
            }

            yield return new WaitForSeconds(time);
        }
    }

    private void RotateToTarget()
    {
        Vector2 targetDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void RotateRandomly()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, 0, randomAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            // move chicken
            transform.position += speed * Time.deltaTime * transform.right;

            if (wasCaught)
            {
                // check if chicken is facing outside of the stop layer
                RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * 1.5f), transform.right, speed * Time.deltaTime, stopLayer);
                if (hit.collider == null)
                {
                    isWalking = false;
                }
            } 
            else
            {
                // check if chicken is on screen
                if (!hasBeenOnScreen && Mathf.Abs(transform.position.x) <= screenBounds.x && Mathf.Abs(transform.position.y) <= screenBounds.y)
                {
                    hasBeenOnScreen = true;
                }

                // check if chicken has reached the screen bounds and has been on screen before
                if (hasBeenOnScreen && (Mathf.Abs(transform.position.x) > screenBounds.x || Mathf.Abs(transform.position.y) > screenBounds.y))
                {
                    isWalking = false; // Stop the chicken
                }
            }
        }

        if (!wasCaught)
        {
            UpdateTimer();
        }

        // set rotation of the child
        Transform spriteTransform = transform.GetChild(0);
        if (spriteTransform != null)
        {
            spriteTransform.rotation = Quaternion.Euler(0, 0, 0);
        }

        UpdateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & stopLayer) != 0)
        {
            isWalking = false;
        }
    }

    private void UpdateTimer()
    {
        // update timer
        timer += Time.deltaTime;
        
        // check if chicken is dead
        if (timer > lifetime)
        {
            gameLogicScript.GameOver("Time expired.");
        }
        
        
        // check if chicken is close to dying
        // execute damage flash
        if (timer <= firstDieAni)
        {
            damagFlash.CallDamageFlash();
            //float leftOvertimeFrame = 1f - closeToDying;
        }
        

    }
    
    private bool CheckForOverlap(string tagToCheck)
    {
        // define radius for overlap check
        float radius = 0.5f;

        // perform overlap check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, stopLayer);

        // search for tag
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(tagToCheck))
                return true;
        }

        return false;
    }

    // destroys itself if it gets out of bounce (just in case)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // control animations
    private void UpdateAnimation()
    {
        // if there are no animations
        if (animator == null)
        {
            Debug.LogWarning("Warning: There is no animation controller!");
            return;
        }
           
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isDragged", dragAndDropScript.Dragging);
    }

    // public methods
    public bool ChickenStableMatch()
    {
        switch (color)
        {
            case ChickenColor.Red:
                return CheckForOverlap("RedStable");
            case ChickenColor.Blue:
                return CheckForOverlap("BlueStable");
            default:
                Debug.LogError("Unknown Stable Color");
                return false;
        }
    }
}
