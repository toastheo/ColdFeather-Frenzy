using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        private new Light2D light;
        [SerializeField] private Gradient lightColorGradient;

        public static WorldLight Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeLight();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Subscribe to WorldTime changes
            WorldTime.Instance.SubscribeToTimeChange(OnWorldTimeChanged);
            UpdateLightColor(WorldTime.currentTime);
        }

        private void OnDestroy()
        {
            if (WorldTime.Instance != null)
            {
                WorldTime.Instance.UnsubscribeFromTimeChange(OnWorldTimeChanged);
            }
        }

        private void InitializeLight()
        {
            light = GetComponent<Light2D>();
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            UpdateLightColor(newTime);
        }

        private void UpdateLightColor(TimeSpan currentTime)
        {
            light.color = lightColorGradient.Evaluate(CalculateTimePercentage(currentTime));
        }

        private float CalculateTimePercentage(TimeSpan time)
        {
            return (float)time.TotalMinutes % WorldTimeConstant.MinutesInDay / WorldTimeConstant.MinutesInDay;
        }


    }
}