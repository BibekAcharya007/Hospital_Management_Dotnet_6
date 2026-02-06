namespace Hospital_Management.Api.DTOs.Labs
{
    public class LabTestDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class CreateLabTestDto
    {
        public string? Name { get; set; }
    }

    public class UpdateLabTestDto
    {
        public string? Name { get; set; }
    }
}
