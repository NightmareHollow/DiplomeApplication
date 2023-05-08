using System;
using GameCore.ScreenManagement.ScreenCollection;
using GameCore.ScreenManagement.ScreensControl;
using GameCore.Services.Infrastructure;
using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace UI.MainMenu.Infrastructure
{
    public class MainMenuScreensControl : MonoBehaviourService
    {
        [Header("References")]

        [SerializeField] private UIScreen defaultScreen;

        private ScreensSwitcher screensSwitcher;
        
        private UIScreen activeScreen;
        private UIScreen prevCachedScreen;

        private bool initialized;

        private void Start()
        {
            InitializeScreensControl();
        }

        public void ActivateScreen(UIScreen screenToActivate, Action completeCallback = null)
        {
            screensSwitcher.SwitchScreens(activeScreen, 
                screenToActivate, completeCallback, () =>
                {
                    prevCachedScreen = activeScreen;
                    activeScreen = screenToActivate;
                });
        }

        public void ActivateCachedScreen(Action completeCallback = null)
        {
            screensSwitcher.SwitchScreens(activeScreen, 
                prevCachedScreen, completeCallback, () =>
                {
                    (activeScreen, prevCachedScreen) = (prevCachedScreen, activeScreen);
                });
        }

        public void ActivateDefaultScreen(Action completeCallback = null)
            => ActivateScreen(defaultScreen, completeCallback);

        private void InitializeScreensControl()
        {
            if (initialized)
                return;
            
            screensSwitcher = MonoBehaviourServicesContainer.GetService<ScreensSwitcher>();
            MonoBehaviourServicesContainer.AddService(this);

            ActivateScreen(defaultScreen);

            initialized = true;
        }

        private void OnDestroy()
        {
            if (initialized)
            {
                MonoBehaviourServicesContainer.RemoveService(this);
            }
        }
    }
}