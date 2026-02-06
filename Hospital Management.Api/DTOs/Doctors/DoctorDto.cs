namespace Hospital_Management.Api.DTOs.Doctors
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int DepartmentId { get; set; }
        public int SpecializationId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SpecializationName { get; set; }
    }

    public class CreateDoctorDto
    {
        public string? FullName { get; set; }
        public int DepartmentId { get; set; }
        public int SpecializationId { get; set; }
    }

    public class UpdateDoctorDto
    {
        public string? FullName { get; set; }
        public int DepartmentId { get; set; }
        public int SpecializationId { get; set; }
    }
}
