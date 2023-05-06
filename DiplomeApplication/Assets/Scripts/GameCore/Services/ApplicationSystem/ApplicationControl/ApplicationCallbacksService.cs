using System;
using GameCore.Services.ServiceStructure;

namespace Multiplayer.Scripts.Game.ApplicationSystem.ApplicationControl
{
    public class ApplicationCallbacksService : InstantMonoBehaviourService
    {
        private bool applicationFocus;
        
        private bool initialized;

        public event Action<bool> OnApplicationFocusChangedEvent;
        public event Action OnApplicationQuitEvent;
        
        public override void InitializeService()
        {
            if (initialized)
                return;

            initialized = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            applicationFocus = hasFocus;
            RaiseApplicationFocusChanged();
        }

        private void OnApplicationQuit() 
            => RaiseApplicationQuit();

        private void RaiseApplicationFocusChanged() 
            => OnApplicationFocusChangedEvent?.Invoke(applicationFocus);

        private void RaiseApplicationQuit() 
            => OnApplicationQuitEvent?.Invoke();
    }
}