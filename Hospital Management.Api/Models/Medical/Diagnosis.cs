namespace Hospital_Management.Api.Models.Medical
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }

        public string? Description { get; set; }
       public MedicalRecord? MedicalRecord { get; set; }
    }
}
