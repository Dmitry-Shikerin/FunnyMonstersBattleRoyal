using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.Frameworks.MyGameCreator.SkyAndWeathers.Domain
{
    [CreateAssetMenu(fileName = "SkyAndWeatherCollector", menuName = "Configs/SkyAndWeatherCollector", order = 51)]
    public class SkyAndWeatherCollector : ConfigCollector<SkyAndWeatherConfig>
    {
        [Range(20, 100)]
        [SerializeField] private float _dayTime = 50f;
        [SerializeField] private bool _isDay;
        [SerializeField] private bool _isNight;
        [SerializeField] private bool _isRain;
        
        public float DayTime => _dayTime;
        public bool IsDay => _isDay;
        public bool IsNight => _isNight;
        public bool IsRain => _isRain;
    }
}