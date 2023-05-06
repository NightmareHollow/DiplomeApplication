using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace Multiplayer.Scripts.Game.ApplicationSystem.ApplicationSetUp
{
    public class ApplicationSetUpService : InstantMonoBehaviourService
    {
        private bool initialized;
        
        public override void InitializeService()
        {
            if (initialized)
                return;

            SetUpApplication();
            
            initialized = true;
        }

        private void SetUpApplication()
        {
            
#if !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Input.multiTouchEnabled = false;
        }
    }
}