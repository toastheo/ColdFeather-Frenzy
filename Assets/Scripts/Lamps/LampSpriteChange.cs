using System;
using UnityEngine;

namespace Lamps
{
    public class LampSpriteChange : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer; // original Sprite
        public Sprite daySprite;
        public Sprite nightSprite; 
        
        private TimeSpan currentTime; 
    
        private void ChangeSprite(bool isDaytime)
        {
            spriteRenderer.sprite = isDaytime ? daySprite : nightSprite;
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
    
        // Update is called once per frame
        private void Update()
        {
            currentTime = WorldTime.WorldTime.currentTime; // get current Time from World Time
    
            if (currentTime.Hours >= DayNightConstant.DayStart && currentTime.Hours < DayNightConstant.DayEnd)
            {
                ChangeSprite(true); // day Sprite
            }
            else
            {
                ChangeSprite(false); // nigh Sprite
            }
        }
        
    
    }

}
