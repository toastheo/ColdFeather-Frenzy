using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        
        [SerializeField] private float dayLenght; // in seconds
        
        public static TimeSpan currentTime; /// <summary>
                                     /// Need to be public for Lamps
                                     /// </summary>
        private float inGameMinuteLenght => dayLenght / WorldTimeConstant.MinutesInDay;
    
        private void Start()
        {
            currentTime = TimeSpan.FromHours(12); // Start first Day at 12:00
            StartCoroutine(AddMinute());
        }
    
        private IEnumerator AddMinute()
        {
            currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, currentTime);
            yield return new WaitForSeconds(inGameMinuteLenght);
            StartCoroutine(AddMinute());
            //print(currentTime);

        }
    
    }

}
