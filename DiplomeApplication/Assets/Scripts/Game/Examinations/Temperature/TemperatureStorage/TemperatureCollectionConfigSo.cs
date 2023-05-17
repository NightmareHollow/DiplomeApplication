using GameCore.ScriptableObjectsManagement;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Examinations.Temperature.TemperatureStorage
{
    [CreateAssetMenu(fileName = "Temperature Collection Config", 
        menuName = SoPathConstants.SoTemperature + "Temperature Collection Config")]
    public class TemperatureCollectionConfigSo : ScriptableObject
    {
        [Min(0f), SerializeField] private float _minAvailableDeltaValue;
        [Min(0f), SerializeField] private float _maxAvailableDeltaValue;

        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private float _checkDelay = 0.6f;
        [SerializeField] private float _checkTime = 8f;

        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private float _additionalValue = 4.5f;

        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private float _minTemperature = 36.1f;
        [SerializeField] private float _maxTemperature = 37f;


        public float MinAvailableDeltaValue => _minAvailableDeltaValue;
        public float MaxAvailableDeltaValue => _maxAvailableDeltaValue;

        public float CheckDelay => _checkDelay;
        public float CheckTime => _checkTime;

        public float AdditionalValue => _additionalValue;

        public float GetRandomTemperature()
            => Random.Range(_minTemperature, _maxTemperature);
    }
}