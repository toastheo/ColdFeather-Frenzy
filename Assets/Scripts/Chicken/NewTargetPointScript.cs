using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chicken
{
    public class NewTargetPointScript : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float radius = 2f;
    
        private Vector3 startPosition;
    
        // Start is called before the first frame update
        void Start()
        {
            // store original position of the gameobject
            startPosition = transform.position;
        }
    
        // Update is called once per frame
        void Update()
        {
            // use trigonometry for circular motion
            float xPosition = Mathf.Cos(Time.time * speed) * radius;
            float yPosition = Mathf.Sin(Time.time * speed) * radius;
    
            // update position
            transform.position = startPosition + new Vector3(xPosition, yPosition, 0);
        }
    }

}
