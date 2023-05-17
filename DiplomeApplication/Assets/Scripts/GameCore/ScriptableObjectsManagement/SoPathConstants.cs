namespace GameCore.ScriptableObjectsManagement
{
    public static class SoPathConstants
    {
        private const string SoCreate = "DiplomeApplication/Anton Cerneavschii/";

        private const string SoExaminations = SoCreate + "Examinations/";
        private const string SoPatients = SoCreate + "Patients/";

        public const string SoTemperature = SoExaminations + "Temperature/";
        public const string SoTonometr = SoExaminations + "Tonometr/";
        public const string SoPulseOximeter = SoExaminations + "Pulse Oximeter/";
        public const string SoPatientPreset = SoPatients + "Patients Preset/";
    }
}