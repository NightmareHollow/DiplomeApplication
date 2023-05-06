using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.Utilities;
using GameCore.Services.Infrastructure;
using GameCore.Services.ServiceStructure;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.Services.ServicesInstallation
{
    public class MonoBehaviourServicesInstaller : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private List<InstantMonoBehaviourService> instantMonoBehaviourServices =
            new List<InstantMonoBehaviourService>();

        [SerializeField] private List<AsynchronousMonoBehaviourService> asynchronousMonoBehaviourServices = 
            new List<AsynchronousMonoBehaviourService>();

        [Header("Values")]

        [SerializeField] private bool dontDestroyOnLoad;

        private bool initialized;

        public bool MarkedAsDontDestroy => dontDestroyOnLoad;

        public async Task InitializeServices(CancellationToken cancellationToken)
        {
            if (initialized)
                return;
            
            InitializeInstantServices();
            await InitializeAsynchronousServices(cancellationToken);

            if (dontDestroyOnLoad) 
                DontDestroyOnLoad(gameObject);

            initialized = true;
        }

        private void InitializeInstantServices()
        {
            foreach (InstantMonoBehaviourService instantService in instantMonoBehaviourServices)
            {
                instantService.InitializeService();
                TryInjectService(instantService);
            }
        }

        private async Task InitializeAsynchronousServices(CancellationToken cancellationToken)
        {
            foreach (AsynchronousMonoBehaviourService asyncService in asynchronousMonoBehaviourServices)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
                
                await asyncService.InitializeService(cancellationToken);
                TryInjectService(asyncService);
            }
        }

        private void TryInjectService<T>(T serviceToInject) where T : MonoBehaviourService
        {
            if (!serviceToInject.AutoInjectService)
                return;
            
            MonoBehaviourServicesContainer.AddService(serviceToInject);
        }

#if UNITY_EDITOR

        [Button("(Re)Load Services")]
        private void ReloadServices()
        {
            instantMonoBehaviourServices.Clear();
            asynchronousMonoBehaviourServices.Clear();
            
            instantMonoBehaviourServices.LoadComponentsOfTypeFromParent(transform);
            asynchronousMonoBehaviourServices.LoadComponentsOfTypeFromParent(transform);
            
            EditorExtensions.MarkObjectDirty(this);
            EditorExtensions.SaveAndRefreshAssets();
        }
        
#endif
    }
}