using DG.Tweening;
using GameCore.ScreenManagement.ScreenCollection;
using GameCore.Services.Infrastructure;
using NaughtyAttributes;
using UI.MainMenu.Infrastructure;
using UnityEngine;

namespace UI.MainMenu.ExaminationPreparationScreen
{
    public class ExaminationPreparationScreenControl : TweenPlayerUIScreen
    {
        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private UIScreen _screenToMove;

        [Header("Values")]

        [Min(0f), SerializeField] private float _activationDelay = 5f;

        private MainMenuScreensControl mainMenuScreensControl;

        private Tween delayTween;

        private bool initialized;

        private void OnEnable()
        {
            Initialize();
            
            ToDefaultState();
            delayTween = DOVirtual.DelayedCall(_activationDelay, OnDelayEnd);
        }

        private void OnDelayEnd()
        {
            mainMenuScreensControl.ActivateScreen(_screenToMove);
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
            delayTween.Kill();
        }

        private void OnDisable()
        {
            ToDefaultState();
        }
    }
}