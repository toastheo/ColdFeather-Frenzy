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
        
        public void StopTime()
        {
            if (timeCoroutine != null)
            {
                StopCoroutine(timeCoroutine);
                timeCoroutine = null;
            }
        }

        public void ResetTime()
        {
            currentTime = TimeSpan.FromHours(12); // Reset to 12:00
            WorldTimeChanged?.Invoke(this, currentTime);
        }

        public void StartTime()
        {
            if (timeCoroutine == null)
            {
                timeCoroutine = StartCoroutine(AddMinute());
            }
        }

        private IEnumerator AddMinute()
        {
            while (true)
            {
                currentTime += TimeSpan.FromMinutes(1);
                //print("current Time: " + currentTime);
                WorldTimeChanged?.Invoke(this, currentTime);
                yield return new WaitForSeconds(inGameMinuteLength);
            }
        }
        
        public void SubscribeToTimeChange(EventHandler<TimeSpan> listener)
        {
            WorldTimeChanged += listener;
        }

        public void UnsubscribeFromTimeChange(EventHandler<TimeSpan> listener)
        {
            WorldTimeChanged -= listener;
        }
    }
}