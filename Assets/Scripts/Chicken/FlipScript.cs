using UnityEngine;

namespace Chicken
{
    public class FlipScript : MonoBehaviour
    {
        private float previousPositionX;
        private SpriteRenderer spriteRenderer;
        private SpriteRenderer childRenderer;
        
        [SerializeField] private ChickenBehaviourScript chickenBehaviourScript;
        [SerializeField] private bool flipChild = false;
    
        // Start is called before the first frame update
        void Start()
        {
            // init previous position with start position
            previousPositionX = transform.position.x;
    
            // get spriteRenderer
            spriteRenderer = GetComponent<SpriteRenderer>();
    
            // get childRenderer
            if (flipChild)
                childRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    
        // Update is called once per frame
        void Update()
        {
            // get current position
            float currentPositionX = transform.position.x;
    
            // compare current position with previous one
            if (currentPositionX > previousPositionX )
            {
                // move to the right and check that sprite isn't flipped
                if (chickenBehaviourScript == null || chickenBehaviourScript.IsWalking)
                {
                    spriteRenderer.flipX = false;
                    if (childRenderer != null)
                        childRenderer.flipX = false;
                }
            }
            else if (currentPositionX < previousPositionX )
            {
                // move to the left and flip sprite
                if (chickenBehaviourScript == null || chickenBehaviourScript.IsWalking)
                {
                    spriteRenderer.flipX = true;
                    if (childRenderer != null)
                        childRenderer.flipX = true;
                }
            }
    
            // update previous position for the next frame
            previousPositionX = currentPositionX;
        }
    }
}

