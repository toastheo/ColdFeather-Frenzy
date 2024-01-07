using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chicken
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChickenSortingSystem : MonoBehaviour
    {
        private SpriteRenderer objectRenderer;
        private Transform nearestDecoration;
    
        // name of the sorting layers you want to switch
        [SerializeField] private string backgroundLayerName = "Chicken Behind";
        [SerializeField] private string foregroundLayerName = "Chicken Before";
    
        // Start is called before the first frame update
        void Start()
        {
            // get spriterenderer
            objectRenderer = GetComponent<SpriteRenderer>();
        }
    
        // Update is called once per frame
        void Update()
        {
            FindNearestDecoration();
    
            if (nearestDecoration != null)
            {
                // check object position relative to the nearest object
                if (transform.position.y - objectRenderer.bounds.extents.y < nearestDecoration.position.y)
                {
                    // set sorting layer to the foreground
                    objectRenderer.sortingLayerName = foregroundLayerName;
                }
                else
                {
                    // set sorting layer to the background
                    objectRenderer.sortingLayerName = backgroundLayerName;
                }
            }
        }
    
        void FindNearestDecoration()
        {
            // find all decoration elements
            GameObject[] decorations = GameObject.FindGameObjectsWithTag("Decoration");
            float nearestDistance = Mathf.Infinity;
    
            foreach (GameObject decoration in decorations)
            {
                // calculate distance between object and decoration
                float distance = (decoration.transform.position - transform.position).sqrMagnitude;
    
                // if distance is smaller than nearestDistance
                if (distance < nearestDistance)
                {
                    nearestDecoration = decoration.transform;
                    nearestDistance = distance;
                }
            }
    
        }
    }
}
