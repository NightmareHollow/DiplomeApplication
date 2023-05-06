using System.Collections.Generic;
using GameCore.CustomExtensions.Collections;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.Utilities;
using GameCore.Infrastructure;
using GameCore.SceneManagement.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.SceneManagement.SceneStorage
{
    [CreateAssetMenu(fileName = "Scene Container", 
        menuName = GameCoreSoPaths.SoSceneCollection + "Scene Container")]
    public class SceneContainerSo : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.DefaultAsset folderToLoad;
#endif
        
        [SerializeField] private GenericDictionary<SceneType, SceneSo> scenesDict = 
            new GenericDictionary<SceneType, SceneSo>();

        public int Count => scenesDict.Count;

        public bool ContainsKey(SceneType key)
            => scenesDict.ContainsKey(key);

        public SceneSo this[SceneType key]
            => scenesDict[key];

#if UNITY_EDITOR

        [Button("(Re)Load Scenes")]
        private void ReloadScenes()
        {
            scenesDict.Clear();

            List<SceneSo> loadedScenes = EditorExtensions.SearchFilesOfTypeInAsset<SceneSo>(folderToLoad);

            foreach (SceneSo scene in loadedScenes)
            {
                if (scene.SceneType == SceneType.None)
                    continue;

                scenesDict.AddOrApplyElement(scene.SceneType, scene);
            }
            
            EditorExtensions.MarkObjectDirty(this);
            EditorExtensions.SaveAndRefreshAssets();
        }
        
#endif
    }
}