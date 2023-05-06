using GameCore.CustomExtensions.ObjectExtensions;
using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace GameCore.ScreenManagement.ScreenTransition.ScreenBlock
{
    public class ScreenBlocker : InstantMonoBehaviourService
    {
        [Header("References")]

        [SerializeField] private GameObject screenBlockPrefab;

        private GameObject screenBlocker;

        private bool initialized;
        
        public override void InitializeService()
        {
            if (initialized)
                return;
            
            InitializeBlocker();

            initialized = true;
        }

        public void ChangeBlockScreenState(bool isActiveState)
            => ChangeBlockerState(isActiveState);

        private void InitializeBlocker()
        {
            screenBlocker = CustomObjectExtensions.SmartInstantiate(screenBlockPrefab);
            DontDestroyOnLoad(screenBlocker);
            ChangeBlockerState(false);
        }

        private void ChangeBlockerState(bool enable)
            => screenBlocker.SetActive(enable);
    }
}