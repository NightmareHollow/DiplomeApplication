using System.Collections;
using GameCore.CustomExtensions.Utilities;
using GameCore.ScreenManagement.ScreenCollection;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using UI.MainMenu.Infrastructure;
using UnityEngine;

namespace UI.MainMenu.ExaminationScreen
{
    public class ExaminationScreenWaitControl : ScreenListenerBase
    {
        [Header("References")]

        [SerializeField] private UIScreen _screenToOpen;

        [Header("Values")]

        [Min(0f), SerializeField] private float _minDelay = 7f;
        [Min(0f), SerializeField] private float _maxDelay = 11f;

        private MainMenuScreensControl mainMenuScreensControl;

        private Coroutine waitRoutine;
        private bool initialized;

        public override void ReactOnShown()
        {
            Initialize();
            
            ActivateWaitDelay();
        }

        public override void ReactOnHide()
        {
            ToDefaultState();
        }

        private void ActivateWaitDelay()
        {
            ToDefaultState();

            float randomDelay = Random.Range(_minDelay, _maxDelay);
            waitRoutine = StartCoroutine(WaitRoutine(randomDelay));
        }

        private IEnumerator WaitRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            OnDelayEnd();
            waitRoutine = null;
        }

        private void OnDelayEnd() 
            => mainMenuScreensControl.ActivateScreen(_screenToOpen);

        private void Initialize()
        {
            if (initialized)
                return;

            mainMenuScreensControl = MonoBehaviourServicesContainer.GetService<MainMenuScreensControl>();

            initialized = true;
        }

        private void ToDefaultState()
        {
            this.DeactivateCoroutine(ref waitRoutine);
        }

        private void OnDisable()
        {
            ToDefaultState();
        }
    }
}