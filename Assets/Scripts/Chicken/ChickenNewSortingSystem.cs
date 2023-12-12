using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChickenNewSortingSystem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // get all sprite renderers
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        // filter by sorting layer "Chicken and Deco"
        List<SpriteRenderer> decorationRenderers = renderers.Where(r => r.sortingLayerName == "Chicken and Deco").ToList();

        // sort according to their y-position
        // for player use the bottom center as pivot
        decorationRenderers = decorationRenderers.OrderByDescending(r => 
        {
            float yPos = r.transform.position.y;
            if (r.gameObject.tag == "Player")
            {
                yPos -= r.bounds.extents.y;
            }
            return yPos;
        }).ToList();

        // assign an order in layer value to each object, starting with 0
        for (int i = 0; i < decorationRenderers.Count; i++)
        {
            decorationRenderers[i].sortingOrder = i;
        }
    }
}
