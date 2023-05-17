using Game.Patient.Interface;
using GameCore.Services.Infrastructure;
using GameCore.Services.ServiceStructure;

namespace Game.Patient.PatientControl
{
    public class ActivePatientControlService : InstantMonoBehaviourService
    {
        private PatientsContainerService patientsContainerService;

        private IPatient activePatient;
        
        private bool initialized;

        public IPatient ActivePatient => activePatient;

        public override void InitializeService()
        {
            if (initialized)
                return;

            patientsContainerService = MonoBehaviourServicesContainer.GetService<PatientsContainerService>();

            initialized = true;
        }

        public void RegisterPatient()
        {
            const string cardId = "A";
            IPatient foundPatient = patientsContainerService.FindPatient(cardId);
            if (foundPatient == null)
                return;

            activePatient = foundPatient;
        }
    }
}