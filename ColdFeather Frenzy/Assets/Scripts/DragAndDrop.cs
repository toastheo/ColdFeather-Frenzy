using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool canMove;
    private bool dragging;

    // for checking if mouse if over game object
    Collider2D mouseOverObject;

    // define frame
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;


    // Start is called before the first frame update
    void Start()
    {

        mouseOverObject = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;


    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            // 0 = left Mouse BTN
            // 1 = righte Mouse BTN
            // 2 = middle Mouse BTN
        {
            if (mouseOverObject == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;

            }
            else
            {
                canMove = false;

            }

            if (canMove)
            {
                dragging = true;
            }

        }


        if (dragging)
        {
            // hold mouse postion inside values
            float frameX = Mathf.Clamp(mousePos.x, minX, maxX);
            float frameY = Mathf.Clamp(mousePos.y, minY, maxY);

            this.transform.position = new Vector2(frameX, frameY);
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}
