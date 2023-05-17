using Game.Examinations.PulseOximeter.Storage;
using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace Game.Examinations.PulseOximeter.Control
{
    public class PulseOximeterControlService : InstantMonoBehaviourService
    {
        [SerializeField] private PulseOximeterConfigSo _pulseOximeterConfig;
        
        private float saturationPercent;
        private int patientPulse;
        
        private bool initialized;

        public float SaturationPercent => saturationPercent;
        public int PatientPulse => patientPulse;

        public override void InitializeService()
        {
            if (initialized)
                return;
            
            SetValues();

            initialized = true;
        }

        private void SetValues()
        {
            saturationPercent = _pulseOximeterConfig.GetRandomSaturation();
            patientPulse = _pulseOximeterConfig.GetRandomPulse();
        }
    }
}