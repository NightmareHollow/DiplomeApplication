using System.Collections.Generic;
using Game.Patient.Interface;
using GameCore.Services.ServiceStructure;

namespace Game.Patient.PatientControl
{
    public class PatientsControlService : InstantMonoBehaviourService
    {
        private readonly List<IPatient> patientsQueueList = 
            new List<IPatient>();
        
        private bool initialized;

        public int PatientsCount => patientsQueueList.Count;
        
        public override void InitializeService()
        {
            if (initialized)
                return;
            
            

            initialized = true;
        }

        public void AddPatient(IPatient patient)
        {
            patientsQueueList.Add(patient);
        }

        public void RemoveFirstPatient()
        {
            if (patientsQueueList.Count <= 0)
                return;
            
            patientsQueueList.RemoveAt(0);
        }

        public void RemovePatient(IPatient patient)
        {
            int foundIndex = patientsQueueList.FindIndex(patientCell => patientCell.PatientId == patient.PatientId);
            if (foundIndex < 0)
                return;
            
            patientsQueueList.RemoveAt(foundIndex);
        }

        public IPatient FindPatient(uint patientId)
        {
            int foundIndex = patientsQueueList.FindLastIndex(patientCell => patientCell.PatientId == patientId);
            if (foundIndex < 0)
                return null;

            return patientsQueueList[foundIndex];
        }
    }
}