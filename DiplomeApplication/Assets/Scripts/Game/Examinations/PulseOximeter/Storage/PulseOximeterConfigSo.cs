using GameCore.ScriptableObjectsManagement;
using UnityEngine;

namespace Game.Examinations.PulseOximeter.Storage
{
    [CreateAssetMenu(fileName = "Pulse Oximeter Config", 
        menuName = SoPathConstants.SoPulseOximeter + "Pulse Oximeter Config")]
    public class PulseOximeterConfigSo : ScriptableObject
    {
        [SerializeField] private float _minSaturationPercent = 0.92f;
        [SerializeField] private float _maxSaturationPercent = 0.97f;

        [Space(10f)]

        [SerializeField] private int _minPulseValue = 64;
        [SerializeField] private int _maxPulseValue = 74;

        public float GetRandomSaturation()
            => Random.Range(_minSaturationPercent, _maxSaturationPercent);

        public int GetRandomPulse()
            => Random.Range(_minPulseValue, _maxPulseValue);
    }
}