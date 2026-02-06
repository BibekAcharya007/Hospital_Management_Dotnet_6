namespace Hospital_Management.Api.Models.Patients
{
    public class MedicalRecord
    {
        public int Id { get; set; }

       public int PatientId { get; set; }
       public int DoctorId { get; set; }

       public string? Notes { get; set; }
       public DateTime CreatedAt { get; set; }

       public Patient Patient { get; set; }
       public Doctor Doctor { get; set; }
    }
}
