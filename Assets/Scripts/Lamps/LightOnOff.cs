using System;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

namespace Lamps
{
    public class LightOnOff : MonoBehaviour
    {
        public Light2D light2D; // Reference to the 2D light component

        private TimeSpan currentTime;
    
        private void ToggleLight(bool isDayTime)
        {
            light2D.enabled = !isDayTime; 
        }
        
        private void Start()
        {
            if(light2D == null)
            {
                light2D = GetComponent<Light2D>(); 
            }
        }
    
        private void Update()
        {
            currentTime = WorldTime.WorldTime.currentTime; // Get current Time from World Time
    
            if (currentTime.Hours >= DayNightConstant.DayStart && currentTime.Hours < DayNightConstant.DayEnd)
            {
                ToggleLight(true);
            }
            else
            {
                ToggleLight(false); 
            }
        }
    }
}