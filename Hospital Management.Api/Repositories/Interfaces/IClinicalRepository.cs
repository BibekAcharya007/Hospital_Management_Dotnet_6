using Hospital_Management.Api.Models.Prescriptions;
using Hospital_Management.Api.Models.Labs;

namespace Hospital_Management.Api.Repositories.Interfaces
{
    public interface IClinicalRepository
    {
        // Prescriptions
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription?> GetPrescriptionByIdAsync(int id);
        Task AddPrescriptionAsync(Prescription prescription);
        Task<bool> UpdatePrescriptionAsync(Prescription prescription);
        Task<bool> DeletePrescriptionAsync(int id);

        // Prescription Items
        Task<IEnumerable<PrescriptionItem>> GetPrescriptionItemsByPrescriptionIdAsync(int prescriptionId);
        Task AddPrescriptionItemAsync(PrescriptionItem item);
        Task<PrescriptionItem?> GetPrescriptionItemByIdAsync(int itemId);
        Task<bool> UpdatePrescriptionItemAsync(PrescriptionItem item);
        Task<bool> DeletePrescriptionItemAsync(int itemId);

        // Medicines
        Task<IEnumerable<Medicine>> GetAllMedicinesAsync();
        Task<Medicine?> GetMedicineByIdAsync(int id);
        Task AddMedicineAsync(Medicine medicine);
        Task<bool> UpdateMedicineAsync(Medicine medicine);
        Task<bool> DeleteMedicineAsync(int id);

        // Lab Tests
        Task<IEnumerable<LabTest>> GetAllLabTestsAsync();
        Task<LabTest?> GetLabTestByIdAsync(int id);
        Task AddLabTestAsync(LabTest test);
        Task<bool> UpdateLabTestAsync(LabTest test);
        Task<bool> DeleteLabTestAsync(int id);

        // Lab Results
        Task<IEnumerable<LabResult>> GetAllLabResultsAsync();
        Task<LabResult?> GetLabResultByIdAsync(int id);
        Task AddLabResultAsync(LabResult result);
        Task<bool> UpdateLabResultAsync(LabResult result);
        Task<bool> DeleteLabResultAsync(int id);

        Task SaveChangesAsync();
    }
}
