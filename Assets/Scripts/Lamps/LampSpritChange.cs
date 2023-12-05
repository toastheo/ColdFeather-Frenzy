using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldTime;

public class LampSpritChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // original Sprite
    public Sprite daySprite; // Sprite for 08:00 to 16:00
    public Sprite nightSprite; // Sprite for 16:00 to 08:00

    private TimeSpan currentTime; 

    void ChangeSprite(bool isDaytime)
    {
        spriteRenderer.sprite = isDaytime ? daySprite : nightSprite;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = WorldTime.WorldTime.currentTime; // get current Time from World Time

        if (currentTime.Hours >= 8 && currentTime.Hours < 16)
        {
            ChangeSprite(true); // day Sprite
        }
        else
        {
            ChangeSprite(false); // nigh Sprite
        }
    }
    

}
