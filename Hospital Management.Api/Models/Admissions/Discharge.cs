namespace Hospital_Management.Api.Models.Admissions
{
    public class Discharge
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}
