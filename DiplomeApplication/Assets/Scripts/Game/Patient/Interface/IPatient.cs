namespace Game.Patient.Interface
{
    public interface IPatient
    {
        public string PatientName { get; }
        public uint PatientId { get; }
        public string PatientCardId { get; }
    }
}