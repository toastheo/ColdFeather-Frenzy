using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
    
        private new Light2D light;

        [SerializeField] private WorldTime worldTime;
        [SerializeField] private Gradient gradient;


        private void Awake()
        {
            Debug.Log("Awake World Light");
            light = GetComponent<Light2D>();
            worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy WorldLight");
            worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged (object sender, TimeSpan newTime)
        {
            light.color = gradient.Evaluate(PercentOfTheDay(newTime));
        }

        private float PercentOfTheDay(TimeSpan timeSpan)
        {
            return (float)timeSpan.TotalMinutes % WorldTimeConstant.MinutesInDay / WorldTimeConstant.MinutesInDay;
        }
    }

}
