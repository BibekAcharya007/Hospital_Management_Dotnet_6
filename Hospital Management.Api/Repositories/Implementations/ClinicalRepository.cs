using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.Models.Prescriptions;
using Hospital_Management.Api.Models.Labs;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Repositories.Implementations
{
    public class ClinicalRepository : IClinicalRepository
    {
        private readonly HospitalDbContext _context;

        public ClinicalRepository(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── PRESCRIPTIONS ─────────────

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
            => await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .ToListAsync();

        public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
            => await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddPrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return false;
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── PRESCRIPTION ITEMS ─────────────

        public async Task<IEnumerable<PrescriptionItem>> GetPrescriptionItemsByPrescriptionIdAsync(int prescriptionId)
            => await _context.PrescriptionItems
                .Include(pi => pi.Medicine)
                .Where(pi => pi.PrescriptionId == prescriptionId)
                .ToListAsync();

        public async Task AddPrescriptionItemAsync(PrescriptionItem item)
        {
            _context.PrescriptionItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<PrescriptionItem?> GetPrescriptionItemByIdAsync(int itemId)
            => await _context.PrescriptionItems.FindAsync(itemId);

        public async Task<bool> UpdatePrescriptionItemAsync(PrescriptionItem item)
        {
            _context.PrescriptionItems.Update(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePrescriptionItemAsync(int itemId)
        {
            var item = await _context.PrescriptionItems.FindAsync(itemId);
            if (item == null) return false;
            _context.PrescriptionItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── MEDICINES ─────────────

        public async Task<IEnumerable<Medicine>> GetAllMedicinesAsync()
            => await _context.Medicines.ToListAsync();

        public async Task<Medicine?> GetMedicineByIdAsync(int id)
            => await _context.Medicines.FindAsync(id);

        public async Task AddMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMedicineAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return false;
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── LAB TESTS ─────────────

        public async Task<IEnumerable<LabTest>> GetAllLabTestsAsync()
            => await _context.LabTests.ToListAsync();

        public async Task<LabTest?> GetLabTestByIdAsync(int id)
            => await _context.LabTests.FindAsync(id);

        public async Task AddLabTestAsync(LabTest test)
        {
            _context.LabTests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLabTestAsync(LabTest test)
        {
            _context.LabTests.Update(test);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLabTestAsync(int id)
        {
            var test = await _context.LabTests.FindAsync(id);
            if (test == null) return false;
            _context.LabTests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── LAB RESULTS ─────────────

        public async Task<IEnumerable<LabResult>> GetAllLabResultsAsync()
            => await _context.LabResults
                .Include(r => r.Patient)
                .Include(r => r.LabTest)
                .ToListAsync();

        public async Task<LabResult?> GetLabResultByIdAsync(int id)
            => await _context.LabResults
                .Include(r => r.Patient)
                .Include(r => r.LabTest)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddLabResultAsync(LabResult result)
        {
            _context.LabResults.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLabResultAsync(LabResult result)
        {
            _context.LabResults.Update(result);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLabResultAsync(int id)
        {
            var result = await _context.LabResults.FindAsync(id);
            if (result == null) return false;
            _context.LabResults.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
