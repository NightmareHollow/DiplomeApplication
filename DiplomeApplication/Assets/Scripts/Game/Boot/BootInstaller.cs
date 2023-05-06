using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameCore.SceneManagement.SceneStorage;
using GameCore.Services.ServicesInstallation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Boot
{
    public class BootInstaller : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private MonoBehaviourServicesInstaller _monoBehaviourServicesInstaller;

        [Header("Values")]

        [SerializeField] private SceneSo _sceneToLoad;

        private async void Start()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            
            try
            {
                await InstallServices(token);

                bool deactivateAfterInstall = _monoBehaviourServicesInstaller.MarkedAsDontDestroy;
                OnServicesInitialized(deactivateAfterInstall);
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                tokenSource.Dispose();
            }
        }

        private async Task InstallServices(CancellationToken cancellationToken)
        {
            await _monoBehaviourServicesInstaller.InitializeServices(cancellationToken);
        }

        private async void OnServicesInitialized(bool deactivateAfterInstall)
        {
            string activeSceneName = SceneManager.GetActiveScene().name;

            await SceneManager.LoadSceneAsync(_sceneToLoad.SceneName, LoadSceneMode.Additive);

            Scene loadedScene = SceneManager.GetSceneByName(_sceneToLoad.SceneName);
            SceneManager.SetActiveScene(loadedScene);

            if (!deactivateAfterInstall)
                return;
            
            await SceneManager.UnloadSceneAsync(activeSceneName);
        }
    }
}