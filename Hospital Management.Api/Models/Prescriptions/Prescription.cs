namespace Hospital_Management.Api.Models.Prescriptions
{
    public class Prescription
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime DateIssued { get; set; }
    }
}
