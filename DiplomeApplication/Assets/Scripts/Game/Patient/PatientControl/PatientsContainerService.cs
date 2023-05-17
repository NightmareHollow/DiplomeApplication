using System.Collections.Generic;
using System.Linq;
using Game.Patient.Interface;
using Game.Patient.PatientsStorage;
using GameCore.Services.Infrastructure;
using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace Game.Patient.PatientControl
{
    public class PatientsContainerService : InstantMonoBehaviourService
    {
        [SerializeField] private PatientsPresetsContainerSo _patientsPresetsContainer;

        [SerializeField] private int _minQueueCount = 1;
        [SerializeField] private int _maxQueueCount = 6;

        private PatientsControlService patientsControlService;
        
        private bool initialized;
        
        public override void InitializeService()
        {
            if (initialized)
                return;

            patientsControlService = MonoBehaviourServicesContainer.GetService<PatientsControlService>();
            
            InitializePatientsQueue();

            initialized = true;
        }

        public IPatient FindPatient(uint patientId)
        {
            if (!_patientsPresetsContainer.ContainsKey(patientId))
                return null;

            return _patientsPresetsContainer[patientId];
        }
        
        public IPatient FindPatient(string cardId)
        {
            KeyValuePair<uint, PatientPresetConfigSo> foundPatient =
                _patientsPresetsContainer.PatientPresetConfigs.First(
                    config => config.Value.PatientCardId == cardId);

            return foundPatient.Value == null ? null : foundPatient.Value;
        }

        private void InitializePatientsQueue()
        {
            int patientsCount = _patientsPresetsContainer.PatientsKeys.Count;
            
            int randomQueueLength = Random.Range(_minQueueCount, _maxQueueCount + 1);
            for (int i = 0; i < randomQueueLength; i++)
            {
                int randomIndex = Random.Range(0, patientsCount);

                int index = 0;
                foreach (uint patientId in _patientsPresetsContainer.PatientsKeys)
                {
                    if (index >= randomIndex)
                    {
                        AddPatient(patientId);
                        break;
                    }

                    ++index;
                }
            }
        }

        private void AddPatient(uint patientId)
        {
            IPatient patientToAdd = _patientsPresetsContainer[patientId];
            patientsControlService.AddPatient(patientToAdd);
        }
    }
}