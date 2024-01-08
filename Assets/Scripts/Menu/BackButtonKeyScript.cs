using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonKeyScript : MonoBehaviour
{
    private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        backButton = GetComponent<Button>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            backButton.onClick.Invoke();
        }
    }
}
