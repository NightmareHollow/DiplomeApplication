using Game.Patient.Interface;
using GameCore.ScriptableObjectsManagement;
using UnityEngine;

namespace Game.Patient.PatientsStorage
{
    [CreateAssetMenu(fileName = "Patient Preset Config", 
        menuName = SoPathConstants.SoPatientPreset + "Patient Preset Config")]
    public class PatientPresetConfigSo : ScriptableObject, IPatient
    {
        [SerializeField] private string _patientName;

        [SerializeField] private uint _patientId;

        [SerializeField] private string _patientCardId;

        public string PatientName => _patientName;
        public uint PatientId => _patientId;
        public string PatientCardId => _patientCardId;
    }
}