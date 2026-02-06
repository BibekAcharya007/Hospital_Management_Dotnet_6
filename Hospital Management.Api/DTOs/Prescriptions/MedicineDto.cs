namespace Hospital_Management.Api.DTOs.Prescriptions
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class CreateMedicineDto
    {
        public string? Name { get; set; }
    }

    public class UpdateMedicineDto
    {
        public string? Name { get; set; }
    }
}
