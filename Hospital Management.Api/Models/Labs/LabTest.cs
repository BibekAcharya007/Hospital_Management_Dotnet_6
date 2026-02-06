namespace Hospital_Management.Api.Models.Labs
{
    public class LabTest
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Navigation properties
        public ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();
    }
}
