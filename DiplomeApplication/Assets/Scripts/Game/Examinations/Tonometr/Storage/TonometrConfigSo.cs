using GameCore.ScriptableObjectsManagement;
using UnityEngine;

namespace Game.Examinations.Tonometr.Storage
{
    [CreateAssetMenu(fileName = "Tonometr Config", 
        menuName = SoPathConstants.SoTonometr + "Tonometr Config")]
    public class TonometrConfigSo : ScriptableObject
    {
        [SerializeField] private int _minMaxValue = 108;
        [SerializeField] private int _maxMaxValue = 116;
        
        [SerializeField] private int _minMinValue = 65;
        [SerializeField] private int _maxMinValue = 73;

        public int GetRandomMaxValue() 
            => Random.Range(_minMaxValue, _maxMaxValue);

        public int GetRandomMinValue() 
            => Random.Range(_minMinValue, _maxMinValue);
    }
}