using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviourScript : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // find target
        target = GameObject.FindWithTag("TargetPoint");

        // rotate chicken in direction of the target
        Vector2 targetDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        // move chicken
        transform.position += speed * Time.deltaTime * transform.right;
    }

    // destroys itself if it gets out of bounce (just in case)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
