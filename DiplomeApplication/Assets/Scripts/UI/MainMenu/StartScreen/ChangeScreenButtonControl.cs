using GameCore.CustomExtensions.UIAnimationsPresets.Button;
using GameCore.ScreenManagement.ScreenCollection;
using GameCore.Services.Infrastructure;
using UI.MainMenu.Infrastructure;
using UnityEngine;

namespace UI.MainMenu.StartScreen
{
    public class ChangeScreenButtonControl : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private SmoothTapButtonAnim _smoothTapButtonAnim;

        [Space(10f)]

        [SerializeField] private UIScreen _screenToMove;

        private MainMenuScreensControl mainMenuScreensControl;

        private void Awake()
        {
            mainMenuScreensControl = MonoBehaviourServicesContainer.GetService<MainMenuScreensControl>();
            
            _smoothTapButtonAnim.TargetButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            mainMenuScreensControl.ActivateScreen(_screenToMove);
        }
    }
}