using GameCore.ScreenManagement.ScreenCollection;
using GameCore.Services.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.ScreenManagement.ScreensControl
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private UIScreen screen1;
        [SerializeField] private UIScreen screen2;

        private ScreensSwitcher screensSwitcher;

        private UIScreen openedScreen;
        
        private void Awake()
        {
            screensSwitcher = MonoBehaviourServicesContainer.GetService<ScreensSwitcher>();
        }


        [Button("Change Screens")]
        private void ChangeScreen()
        {
            UIScreen targetScreen = (openedScreen == screen1) ? screen2 : screen1;
            screensSwitcher.SwitchScreens(openedScreen, targetScreen, onCompleteFrom: () =>
            {
                openedScreen = targetScreen;
            });
        }
    }
}