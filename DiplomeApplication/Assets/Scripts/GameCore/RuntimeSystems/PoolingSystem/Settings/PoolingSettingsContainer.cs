using System.Collections.Generic;
using System.Linq;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.DebugSystemExtensions;
using GameCore.Infrastructure;
using GameCore.RuntimeSystems.PoolingSystem.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.RuntimeSystems.PoolingSystem.Settings
{
    [CreateAssetMenu(fileName = "PoolingSettings", 
        menuName = GameCoreSoPaths.SoPoolingSystem + "PoolingSettingsContainer")]
    public class PoolingSettingsContainer : ScriptableObject
    {
        [SerializeField] private bool usePoolingService = true;
        
        [HorizontalLine(color: EColor.Green)]
        
        [SerializeField] private List<PoolSettings> poolsSettings = 
            new List<PoolSettings>();

        public bool UsePoolingService => usePoolingService;
        
        public List<PoolSettings> PoolsSettingsList => poolsSettings;

#if UNITY_EDITOR

        private void OnValidate()
        {
            IEnumerable<PoolSettings> uncheckedSettings = poolsSettings.Where(
                // ReSharper disable once SuspiciousTypeConversion.Global
                settings => settings.Original != null && !(settings.Original is IPoolComponent));

            int settingsCount = 0;

            foreach (PoolSettings poolSettings in uncheckedSettings)
            {
                bool foundComponent = poolSettings.Original.TryGetComponent(out IPoolComponent poolComponent);

                if (foundComponent)
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    poolSettings.Original = (Component) poolComponent;
                }
                else
                {
                    DebugExtensions.DebugMessage(poolSettings.Original.name + " doesn't have any pool component!", 
                        DebugExtensions.MessageType.Error);

                    poolSettings.Original = null;
                }

                ++settingsCount;
            }

            //If there are some settings, Save assets
            if (settingsCount > 0)
            {
                EditorExtensions.MarkObjectDirty(this);
                EditorExtensions.SaveAndRefreshAssets();
            }
        }

#endif
    }
}