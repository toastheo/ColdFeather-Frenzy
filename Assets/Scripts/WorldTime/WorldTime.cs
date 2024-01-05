using System;
using System.Collections;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        public static TimeSpan currentTime;
        
        [SerializeField] private float dayLength; // in seconds
        private float inGameMinuteLength => dayLength / WorldTimeConstant.MinutesInDay;
        private Coroutine timeCoroutine;

        public static WorldTime Instance { get; private set; }
        // create singleton throughout the game -> access globally 
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            currentTime = TimeSpan.FromHours(12); // Start first Day at 12:00
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
            yield return new WaitForSeconds(inGameMinuteLength);
            StartCoroutine(AddMinute());
        }
    
    }

}
