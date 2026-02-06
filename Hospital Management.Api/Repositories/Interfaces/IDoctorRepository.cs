using Hospital_Management.Api.Models.Doctors;

namespace Hospital_Management.Api.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        // Doctors
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(Doctor doctor);
        Task<bool> UpdateDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(int id);

        // Departments
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(int id);

        // Specializations
        Task<IEnumerable<DoctorSpecialization>> GetAllSpecializationsAsync();
        Task<DoctorSpecialization?> GetSpecializationByIdAsync(int id);
        Task AddSpecializationAsync(DoctorSpecialization specialization);
        Task<bool> UpdateSpecializationAsync(DoctorSpecialization specialization);
        Task<bool> DeleteSpecializationAsync(int id);

        Task SaveChangesAsync();
    }
}
