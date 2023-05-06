using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.Infrastructure;
using GameCore.SceneManagement.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.SceneManagement.SceneStorage
{
    [CreateAssetMenu(fileName = "Scene", menuName = GameCoreSoPaths.SoSceneCollection + "Scene")]
    public class SceneSo : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset attachedSceneAsset;
#endif

        [SerializeField] private SceneType sceneType;
        
        [SerializeField] private string sceneName;
        
        public SceneType SceneType => sceneType;
        public string SceneName => sceneName;

#if UNITY_EDITOR

        [Button("Update Scene Name")]
        private void UpdateSceneName()
        {
            string newName = attachedSceneAsset.name;
            sceneName = newName;
            
            EditorExtensions.MarkObjectDirty(this);
            EditorExtensions.SaveAndRefreshAssets();
        }

        [Button("(Re)Name Asset Name")]
        private void UpdateAssetName()
        {
            string newName = sceneType + " Scene";

            this.TryToRenameAsset(newName);
        }
        
#endif
    }
}