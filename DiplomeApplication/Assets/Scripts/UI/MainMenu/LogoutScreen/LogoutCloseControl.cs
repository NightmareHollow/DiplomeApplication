using DG.Tweening;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using UI.MainMenu.Infrastructure;
using UnityEngine;

namespace UI.MainMenu.LogoutScreen
{
    public class LogoutCloseControl : ScreenListenerBase
    {
        [Header("Values")]

        [Min(0f), SerializeField] private float _closeDelay = 4f;

        private MainMenuScreensControl mainMenuScreensControl;

        private Tween delayTween;
        private bool initialized;

        public override void ReactOnShown()
        {
            Initialize();
            
            ActivateDelay();
        }

        public override void ReactOnHide()
        {
            ToDefaultState();
        }

        private void ActivateDelay()
            => delayTween = DOVirtual.DelayedCall(_closeDelay, OnDelayEnd);

        private void OnDelayEnd() 
            => mainMenuScreensControl.ActivateDefaultScreen();

        private void Initialize()
        {
            if (initialized)
                return;

            mainMenuScreensControl = MonoBehaviourServicesContainer.GetService<MainMenuScreensControl>();

            initialized = true;
        }

        private void ToDefaultState()
        {
            delayTween.Kill();   
        }

        private void OnDisable()
        {
            ToDefaultState();
        }
    }
}