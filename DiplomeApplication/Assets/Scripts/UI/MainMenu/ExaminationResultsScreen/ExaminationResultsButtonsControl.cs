using Game.Patient.Interface;
using Game.Patient.PatientControl;
using GameCore.ScreenManagement.ScreenCollection;
using GameCore.Services.Infrastructure;
using UI.MainMenu.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.ExaminationResultsScreen
{
    public class ExaminationResultsButtonsControl : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _registerButton;

        [Space(10f)]

        [SerializeField] private UIScreen _retryScreenToMove;
        [SerializeField] private UIScreen _registerScreenToMove;

        private MainMenuScreensControl mainMenuScreensControl;
        private PatientsControlService patientsControlService;
        private ActivePatientControlService activePatientControlService;

        private void Awake()
        {
            mainMenuScreensControl = MonoBehaviourServicesContainer.GetService<MainMenuScreensControl>();
            patientsControlService = MonoBehaviourServicesContainer.GetService<PatientsControlService>();
            activePatientControlService = MonoBehaviourServicesContainer.GetService<ActivePatientControlService>();
            
            _retryButton.onClick.AddListener(OnRetryButtonClicked);
            _registerButton.onClick.AddListener(OnRegisterButtonClicked);
        }

        private void OnRetryButtonClicked() 
            => mainMenuScreensControl.ActivateScreen(_retryScreenToMove);

        private void OnRegisterButtonClicked()
        {
            mainMenuScreensControl.ActivateScreen(_registerScreenToMove);

            IPatient activePatient = activePatientControlService.ActivePatient;
            if (activePatient == null)
                return;
            
            patientsControlService.AddPatient(activePatient);
        }
    }
}