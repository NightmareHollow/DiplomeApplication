using System.Collections.Generic;
using Game.Patient.Interface;
using GameCore.CustomExtensions.Collections;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.Utilities;
using GameCore.ScriptableObjectsManagement;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Patient.PatientsStorage
{
    [CreateAssetMenu(fileName = "Patients Presets Container", 
        menuName = SoPathConstants.SoPatientPreset + "Patients Presets Container")]
    public class PatientsPresetsContainerSo : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.DefaultAsset _folderToLoad;
#endif
        
        [SerializeField] private GenericDictionary<uint, PatientPresetConfigSo> _patientPresetConfigs =
            new GenericDictionary<uint, PatientPresetConfigSo>();

        public bool ContainsKey(uint key)
            => _patientPresetConfigs.ContainsKey(key);

        public IPatient this[uint key]
            => _patientPresetConfigs[key];

        public ICollection<uint> PatientsKeys => _patientPresetConfigs.Keys;

        public IDictionary<uint, PatientPresetConfigSo> PatientPresetConfigs => _patientPresetConfigs;

#if UNITY_EDITOR

        [Button("(Re)Load Patients")]
        private void LoadPresets()
        {
            _patientPresetConfigs.Clear();

            List<PatientPresetConfigSo> foundConfigs =
                EditorExtensions.SearchFilesOfTypeInAsset<PatientPresetConfigSo>(_folderToLoad);

            foreach (PatientPresetConfigSo foundConfig in foundConfigs)
            {
                _patientPresetConfigs.AddOrApplyElement(foundConfig.PatientId, foundConfig);
            }
        }
        
#endif
    }
}