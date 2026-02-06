using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.Models.Patients;
using Hospital_Management.Api.Models.Medical;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── PATIENTS ─────────────

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
            => await _context.Patients.ToListAsync();

        public async Task<Patient?> GetPatientByIdAsync(int id)
            => await _context.Patients.FindAsync(id);

        public async Task AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── PATIENT ADDRESSES ─────────────

        public async Task<IEnumerable<PatientAddress>> GetAddressesByPatientIdAsync(int patientId)
            => await _context.PatientAddresses.Where(a => a.PatientId == patientId).ToListAsync();

        public async Task AddAddressAsync(PatientAddress address)
        {
            _context.PatientAddresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task<PatientAddress?> GetAddressByIdAsync(int addressId)
            => await _context.PatientAddresses.FindAsync(addressId);

        public async Task<bool> UpdateAddressAsync(PatientAddress address)
        {
            _context.PatientAddresses.Update(address);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var address = await _context.PatientAddresses.FindAsync(addressId);
            if (address == null) return false;
            _context.PatientAddresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── MEDICAL RECORDS ─────────────

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId)
            => await _context.MedicalRecords.Where(r => r.PatientId == patientId).ToListAsync();

        public async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task<MedicalRecord?> GetMedicalRecordByIdAsync(int recordId)
            => await _context.MedicalRecords.FindAsync(recordId);

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Update(record);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMedicalRecordAsync(int recordId)
        {
            var record = await _context.MedicalRecords.FindAsync(recordId);
            if (record == null) return false;
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }

        // ───────────── DIAGNOSES ─────────────

        public async Task<IEnumerable<Diagnosis>> GetDiagnosesByRecordIdAsync(int recordId)
            => await _context.Diagnoses.Where(d => d.MedicalRecordId == recordId).ToListAsync();

        public async Task AddDiagnosisAsync(Diagnosis diagnosis)
        {
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();
        }

        public async Task<Diagnosis?> GetDiagnosisByIdAsync(int diagnosisId)
            => await _context.Diagnoses.FindAsync(diagnosisId);

        public async Task<bool> UpdateDiagnosisAsync(Diagnosis diagnosis)
        {
            _context.Diagnoses.Update(diagnosis);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDiagnosisAsync(int diagnosisId)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(diagnosisId);
            if (diagnosis == null) return false;
            _context.Diagnoses.Remove(diagnosis);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
