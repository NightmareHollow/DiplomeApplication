using System.Collections;
using GameCore.CustomExtensions.Utilities;
using GameCore.ScreenManagement.ScreenCollection;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using UI.MainMenu.Infrastructure;
using UnityEngine;

namespace UI.MainMenu.WelcomeScreen
{
    public class WelcomeScreenTransitionControl : ScreenListenerBase
    {
        [Header("References")]

        [SerializeField] private UIScreen _screenToMove;

        [SerializeField] private WelcomeScreenPatientInitControl _welcomeScreenPatientInitControl;

        private MainMenuScreensControl mainMenuScreensControl;
        
        private Coroutine inputRoutine;

        private bool activated;
        private bool initialized;

        public override void ReactOnShown()
        {
            Initialize();
            
            activated = true;
            inputRoutine = StartCoroutine(InputRoutine());
        }

        public override void ReactOnHide()
        {
            ToDefaultState();
        }

        private IEnumerator InputRoutine()
        {
            while (activated)
            {
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Mouse2))
                {
                    OnChangeScreenInputRegistered();
                }

                yield return null;
            }
        }

        private void OnChangeScreenInputRegistered()
        {
            if (!activated)
                return;
            
            activated = false;
            
            mainMenuScreensControl.ActivateScreen(_screenToMove);
            _welcomeScreenPatientInitControl.ActivatePatient();
        }
        
        private void Initialize()
        {
            if (initialized)
                return;

            mainMenuScreensControl = MonoBehaviourServicesContainer.GetService<MainMenuScreensControl>();

            initialized = true;
        }

        private void ToDefaultState()
        {
            this.DeactivateCoroutine(ref inputRoutine);

            activated = false;
        }

        private void OnDisable()
        {
            ToDefaultState();   
        }
    }
}