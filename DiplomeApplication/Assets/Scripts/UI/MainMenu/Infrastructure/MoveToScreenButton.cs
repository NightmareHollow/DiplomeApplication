using GameCore.ScreenManagement.ScreenCollection;
using GameCore.Services.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Infrastructure
{
    public class MoveToScreenButton : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private UIScreen _screenToMove;

        [SerializeField] private Button _selectableButton;

        private MainMenuScreensControl mainMenuScreensControl;
        
        private void Awake()
        {
            mainMenuScreensControl = MonoBehaviourServicesContainer.GetService<MainMenuScreensControl>();

            _selectableButton.onClick.AddListener(OnButtonClicked);  
        }

        private void OnButtonClicked() 
            => mainMenuScreensControl.ActivateScreen(_screenToMove);
    }
}