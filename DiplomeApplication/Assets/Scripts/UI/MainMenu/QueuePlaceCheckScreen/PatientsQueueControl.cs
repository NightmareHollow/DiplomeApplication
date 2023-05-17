using System.Collections;
using Game.Patient.PatientControl;
using GameCore.CustomExtensions.Utilities;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using TMPro;
using UnityEngine;

namespace UI.MainMenu.QueuePlaceCheckScreen
{
    public class PatientsQueueControl : ScreenListenerBase
    {
        [SerializeField] private TextMeshProUGUI _patientsQueueText;

        [Header("Values")]

        [SerializeField] private float _minQueueDelay = 8f;
        [SerializeField] private float _maxQueueDelay = 13f;

        private PatientsControlService patientsControlService;

        private Coroutine decreaseQueueRoutine;
        private bool initialized;

        public override void ReactOnShow()
        {
            Initialize();
            
            UpdateVisual();
            decreaseQueueRoutine = StartCoroutine(DecreaseQueueRoutine());
        }

        public override void ReactOnHide()
        {
            ToDefaultState();
        }

        private IEnumerator DecreaseQueueRoutine()
        {
            while (patientsControlService.PatientsCount > 1)
            {
                float randomDelay = Random.Range(_minQueueDelay, _maxQueueDelay);
                yield return new WaitForSeconds(randomDelay);
                
                patientsControlService.RemoveFirstPatient();
                UpdateVisual();
            }
        }

        private void UpdateVisual()
        {
            _patientsQueueText.text = patientsControlService.PatientsCount.ToString();
        }

        private void Initialize()
        {
            if (initialized)
                return;

            patientsControlService = MonoBehaviourServicesContainer.GetService<PatientsControlService>();

            initialized = true;
        }
        
        private void ToDefaultState()
        {
            this.DeactivateCoroutine(ref decreaseQueueRoutine);
        }

        private void OnDisable()
        {
            ToDefaultState();
        }
    }
}