using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool canMove = false;
    private bool dragging = false;

    // for checking if mouse is over game object
    Collider2D mouseOverObject;

    // define frame
    [SerializeField] private Vector2 maxBound = new(8, 4);
    [SerializeField] private Vector2 minBound = new(-8, -4);

    // Start is called before the first frame update
    void Start()
    {
        mouseOverObject = GetComponent<Collider2D>();
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
            canMove = mouseOverObject == Physics2D.OverlapPoint(mousePos);

            if (canMove)
            {
                dragging = true;
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

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}
