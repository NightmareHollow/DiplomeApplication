using GameCore.ScreenManagement.ScreensControl;
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
        
        public override void ReactOnShow()
        {
            UpdateResultsInformation();
        }

        public override void ReactOnHide()
        {
            
        }

        private void UpdateResultsInformation()
        {
            
        }
    }
}