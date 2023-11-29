using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=0nq1ZFxuEJY

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        
        [SerializeField] private float dayLenght; // in seconds
        
        private TimeSpan currentTime;
        private float inGameMinuteLenght => dayLenght / WorldTimeConstant.MinutesInDay;
    
        private void Start()
        {
            StartCoroutine(AddMinute());
        }
    
        private IEnumerator AddMinute()
        {
            currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, currentTime);
            yield return new WaitForSeconds(inGameMinuteLenght);
            StartCoroutine(AddMinute());
        }
    
    }

}
