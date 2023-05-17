using Game.Patient.Interface;
using Game.Patient.PatientControl;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using TMPro;
using UnityEngine;

namespace UI.MainMenu.StartScreen
{
    public class StartScreenPatientActivator : ScreenListenerBase
    {
        [SerializeField] private TextMeshProUGUI _welcomeText;
        
        private ActivePatientControlService activePatientControlService;
        
        private bool initialized;

        public override void ReactOnShow()
        {
            Initialize();
            UpdateVisual();
        }
        
        private void Initialize()
        {
            if (initialized)
                return;

            activePatientControlService = MonoBehaviourServicesContainer.GetService<ActivePatientControlService>();

            initialized = true;
        }

        private void UpdateVisual()
        {
            IPatient activePatient = activePatientControlService.ActivePatient;
            if (activePatient == null)
                return;

            _welcomeText.text = "Welcome, " + activePatient.PatientName + "!";
        }
    }
}