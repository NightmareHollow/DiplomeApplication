using Game.Patient.PatientControl;
using GameCore.Services.Infrastructure;
using UnityEngine;

namespace UI.MainMenu.WelcomeScreen
{
    public class WelcomeScreenPatientInitControl : MonoBehaviour
    {
        private ActivePatientControlService activePatientControlService;
        
        private void Awake()
        {
            activePatientControlService = MonoBehaviourServicesContainer.GetService<ActivePatientControlService>();
        }

        public void ActivatePatient() 
            => activePatientControlService.RegisterPatient();
    }
}