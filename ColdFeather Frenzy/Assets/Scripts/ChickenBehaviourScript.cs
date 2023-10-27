using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviourScript : MonoBehaviour
{
    private GameObject target;

    private bool isWalking = false;
    private bool hasBeenOnScreen = false;

    private Vector2 screenBounds;

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask stopLayer;

    [Header("Time parameters")]
    [SerializeField] private float maxWalkTime = 2f;
    [SerializeField] private float minWalkTime = 1f;
    [SerializeField] private float maxStopTime = 1f;
    [SerializeField] private float minStopTime = 0.5f;  

    // Start is called before the first frame update
    void Start()
    {
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

        StartCoroutine(WalkAndStop());
    }

    // Coroutine to let the chicken walk and stop between random parameters
    IEnumerator WalkAndStop()
    {
        while (true)
        {
            isWalking = !isWalking;

            float time;
            if (isWalking)
            {
                // rotate chicken in direction of the target
                RotateToTarget();
                time = Random.Range(minWalkTime, maxWalkTime);
            }
            else
            {
                time = Random.Range(minStopTime, maxStopTime);
            }

            yield return new WaitForSeconds(time);
        }
    }

    void RotateToTarget()
    {
        Vector2 targetDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            // move chicken
            transform.position += speed * Time.deltaTime * transform.right;

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

    // if it chicken the stop layer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & stopLayer) != 0)
        {
            isWalking = false;
        }
    }

    // destroys itself if it gets out of bounce (just in case)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
