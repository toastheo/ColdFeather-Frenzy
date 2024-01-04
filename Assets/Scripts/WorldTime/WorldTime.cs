using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        public static TimeSpan currentTime;
        
        [SerializeField] private float dayLenght; // in seconds
        private float inGameMinuteLenght => dayLenght / WorldTimeConstant.MinutesInDay;
        private Coroutine timeCoroutine;
        
        private void Start()
        {
            currentTime = TimeSpan.FromHours(12); // Start first Day at 12:00
            timeCoroutine = StartCoroutine(AddMinute());
        }

        public void StopTime()
        {
            if (timeCoroutine != null)
            {
                StopCoroutine(timeCoroutine);
            }
        }

        public void ResetTime()
        {
            currentTime = TimeSpan.FromHours(12); // Start first Day at 12:00
            WorldTimeChanged?.Invoke(this, currentTime);
        }

        public void StartTime()
        {
            timeCoroutine = StartCoroutine(AddMinute());
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
