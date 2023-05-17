using Game.Examinations.PulseOximeter.Control;
using Game.Examinations.Temperature.TemperatureControl;
using Game.Examinations.Tonometr.Control;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using TMPro;
using UnityEngine;

namespace UI.MainMenu.ExaminationResultsScreen
{
    public class ExaminationResultsVisualControl : ScreenListenerBase
    {
        [Header("References")]

        [SerializeField] private TextMeshProUGUI _temperatureText;
        [SerializeField] private TextMeshProUGUI _bloodPressureText;
        [SerializeField] private TextMeshProUGUI _saturationText;
        [SerializeField] private TextMeshProUGUI _pulseRateText;

        private TemperatureControlService temperatureControlService;
        private TonometrControlService tonometrControlService;
        private PulseOximeterControlService pulseOximeterControlService;

        private bool initialized;
        
        public override void ReactOnShow()
        {
            Initialize();
            
            UpdateResultsInformation();
        }

        public override void ReactOnHide()
        {
            
        }

        private void UpdateResultsInformation()
        {
            _temperatureText.text = temperatureControlService.FinalTemperatureValue.ToString("0.00");
            _bloodPressureText.text = tonometrControlService.GetPressure();
            _saturationText.text = Mathf.RoundToInt(pulseOximeterControlService.SaturationPercent * 100f).ToString();
            _pulseRateText.text = pulseOximeterControlService.PatientPulse.ToString();
        }
        
        private void Initialize()
        {
            if (initialized)
                return;

            temperatureControlService = MonoBehaviourServicesContainer.GetService<TemperatureControlService>();
            tonometrControlService = MonoBehaviourServicesContainer.GetService<TonometrControlService>();
            pulseOximeterControlService = MonoBehaviourServicesContainer.GetService<PulseOximeterControlService>();

            initialized = true;
        }

    }
}