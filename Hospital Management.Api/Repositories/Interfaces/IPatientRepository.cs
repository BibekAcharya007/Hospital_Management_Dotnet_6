using Hospital_Management.Api.Models.Patients;
using Hospital_Management.Api.Models.Medical;

namespace Hospital_Management.Api.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        // Patients
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task AddPatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);

        // Patient Addresses
        Task<IEnumerable<PatientAddress>> GetAddressesByPatientIdAsync(int patientId);
        Task AddAddressAsync(PatientAddress address);
        Task<PatientAddress?> GetAddressByIdAsync(int addressId);
        Task<bool> UpdateAddressAsync(PatientAddress address);
        Task<bool> DeleteAddressAsync(int addressId);

        // Medical Records
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId);
        Task AddMedicalRecordAsync(MedicalRecord record);
        Task<MedicalRecord?> GetMedicalRecordByIdAsync(int recordId);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord record);
        Task<bool> DeleteMedicalRecordAsync(int recordId);

        // Diagnoses
        Task<IEnumerable<Diagnosis>> GetDiagnosesByRecordIdAsync(int recordId);
        Task AddDiagnosisAsync(Diagnosis diagnosis);
        Task<Diagnosis?> GetDiagnosisByIdAsync(int diagnosisId);
        Task<bool> UpdateDiagnosisAsync(Diagnosis diagnosis);
        Task<bool> DeleteDiagnosisAsync(int diagnosisId);

        Task SaveChangesAsync();
    }
}
