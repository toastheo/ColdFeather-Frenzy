using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float distance = 2f;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;     // store the start position
    }

    // Update is called once per frame
    void Update()
    {
        // use sinusfunction for an oscillating motion 
        float movement = Mathf.Sin(Time.time * speed) * distance;

        // update position
        transform.position = startPosition + new Vector3(movement, 0, 0);
    }
}
