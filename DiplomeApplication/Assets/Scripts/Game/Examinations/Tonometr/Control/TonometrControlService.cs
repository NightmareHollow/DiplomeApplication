using Game.Examinations.Tonometr.Storage;
using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace Game.Examinations.Tonometr.Control
{
    public class TonometrControlService : InstantMonoBehaviourService
    {
        [SerializeField] private TonometrConfigSo _tonometrConfig;

        private int maxPressure;
        private int minPressure;
        
        private bool initialized;

        public string GetPressure() 
            => maxPressure + " / " + minPressure;
        
        public override void InitializeService()
        {
            if (initialized)
                return;

            SetValue();
            
            initialized = true;
        }

        private void SetValue()
        {
            maxPressure = _tonometrConfig.GetRandomMaxValue();
            minPressure = _tonometrConfig.GetRandomMinValue();
        }
    }
}