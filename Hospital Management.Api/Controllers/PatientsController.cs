using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Data;
using Hospital_Management.Api.DTOs.Patients;
using Hospital_Management.Api.DTOs.Medical;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Patients;
using Hospital_Management.Api.Models.Medical;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PatientsController(HospitalDbContext context)
        {
            _context = context;
        }

        // ───────────── PATIENTS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id, FullName = p.FullName, DOB = p.DOB, Gender = p.Gender,
                    BloodGroup = p.BloodGroup, Allergies = p.Allergies,
                    ChronicConditions = p.ChronicConditions, Phone = p.Phone,
                    Email = p.Email, EmergencyContactName = p.EmergencyContactName,
                    EmergencyContactPhone = p.EmergencyContactPhone, EntryTime = p.EntryTime
                }).ToListAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _context.Patients
                .Where(p => p.Id == id)
                .Select(p => new PatientDto
                {
                    Id = p.Id, FullName = p.FullName, DOB = p.DOB, Gender = p.Gender,
                    BloodGroup = p.BloodGroup, Allergies = p.Allergies,
                    ChronicConditions = p.ChronicConditions, Phone = p.Phone,
                    Email = p.Email, EmergencyContactName = p.EmergencyContactName,
                    EmergencyContactPhone = p.EmergencyContactPhone, EntryTime = p.EntryTime
                }).FirstOrDefaultAsync();

            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FullName = dto.FullName, DOB = dto.DOB, Gender = dto.Gender,
                BloodGroup = dto.BloodGroup, Allergies = dto.Allergies,
                ChronicConditions = dto.ChronicConditions, Phone = dto.Phone,
                Email = dto.Email, EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactPhone = dto.EmergencyContactPhone, EntryTime = DateTime.UtcNow
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return NotFound();

            patient.FullName = dto.FullName; patient.DOB = dto.DOB; patient.Gender = dto.Gender;
            patient.BloodGroup = dto.BloodGroup; patient.Allergies = dto.Allergies;
            patient.ChronicConditions = dto.ChronicConditions; patient.Phone = dto.Phone;
            patient.Email = dto.Email; patient.EmergencyContactName = dto.EmergencyContactName;
            patient.EmergencyContactPhone = dto.EmergencyContactPhone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return NotFound();

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── PATIENT ADDRESSES ─────────────

        [HttpGet("{patientId}/addresses")]
        public async Task<IActionResult> GetAddresses(int patientId)
        {
            var addresses = await _context.PatientAddresses
                .Where(a => a.PatientId == patientId)
                .Select(a => new PatientAddressDto
                {
                    Id = a.Id, PatientId = a.PatientId,
                    AddressLine = a.AddressLine, City = a.City, State = a.State
                }).ToListAsync();

            return Ok(addresses);
        }

        [HttpPost("{patientId}/addresses")]
        public async Task<IActionResult> CreateAddress(int patientId, CreatePatientAddressDto dto)
        {
            var address = new PatientAddress
            {
                PatientId = patientId,
                AddressLine = dto.AddressLine, City = dto.City, State = dto.State
            };

            _context.PatientAddresses.Add(address);
            await _context.SaveChangesAsync();
            return Created($"api/patients/{patientId}/addresses", new PatientAddressDto
            {
                Id = address.Id, PatientId = patientId,
                AddressLine = address.AddressLine, City = address.City, State = address.State
            });
        }

        [HttpPut("addresses/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, UpdatePatientAddressDto dto)
        {
            var address = await _context.PatientAddresses.FindAsync(addressId);
            if (address == null) return NotFound();

            address.AddressLine = dto.AddressLine; address.City = dto.City; address.State = dto.State;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var address = await _context.PatientAddresses.FindAsync(addressId);
            if (address == null) return NotFound();

            _context.PatientAddresses.Remove(address);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── MEDICAL RECORDS ─────────────

        [HttpGet("{patientId}/medical-records")]
        public async Task<IActionResult> GetMedicalRecords(int patientId)
        {
            var records = await _context.MedicalRecords
                .Where(r => r.PatientId == patientId)
                .Select(r => new MedicalRecordDto
                {
                    Id = r.Id, PatientId = r.PatientId, DoctorId = r.DoctorId,
                    Notes = r.Notes, CreatedAt = r.CreatedAt
                }).ToListAsync();

            return Ok(records);
        }

        [HttpPost("{patientId}/medical-records")]
        public async Task<IActionResult> CreateMedicalRecord(int patientId, CreateMedicalRecordDto dto)
        {
            var record = new MedicalRecord
            {
                PatientId = patientId, DoctorId = dto.DoctorId,
                Notes = dto.Notes, CreatedAt = DateTime.UtcNow
            };

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
            return Created($"api/patients/{patientId}/medical-records", new MedicalRecordDto
            {
                Id = record.Id, PatientId = patientId, DoctorId = record.DoctorId,
                Notes = record.Notes, CreatedAt = record.CreatedAt
            });
        }

        [HttpPut("medical-records/{recordId}")]
        public async Task<IActionResult> UpdateMedicalRecord(int recordId, UpdateMedicalRecordDto dto)
        {
            var record = await _context.MedicalRecords.FindAsync(recordId);
            if (record == null) return NotFound();

            record.Notes = dto.Notes;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("medical-records/{recordId}")]
        public async Task<IActionResult> DeleteMedicalRecord(int recordId)
        {
            var record = await _context.MedicalRecords.FindAsync(recordId);
            if (record == null) return NotFound();

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ───────────── DIAGNOSES ─────────────

        [HttpGet("medical-records/{recordId}/diagnoses")]
        public async Task<IActionResult> GetDiagnoses(int recordId)
        {
            var diagnoses = await _context.Diagnoses
                .Where(d => d.MedicalRecordId == recordId)
                .Select(d => new DiagnosisDto
                {
                    Id = d.Id, MedicalRecordId = d.MedicalRecordId, Description = d.Description
                }).ToListAsync();

            return Ok(diagnoses);
        }

        [HttpPost("medical-records/{recordId}/diagnoses")]
        public async Task<IActionResult> CreateDiagnosis(int recordId, CreateDiagnosisDto dto)
        {
            var diagnosis = new Diagnosis
            {
                MedicalRecordId = recordId, Description = dto.Description
            };

            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();
            return Created($"api/patients/medical-records/{recordId}/diagnoses", dto);
        }

        [HttpPut("diagnoses/{diagnosisId}")]
        public async Task<IActionResult> UpdateDiagnosis(int diagnosisId, UpdateDiagnosisDto dto)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(diagnosisId);
            if (diagnosis == null) return NotFound();

            diagnosis.Description = dto.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("diagnoses/{diagnosisId}")]
        public async Task<IActionResult> DeleteDiagnosis(int diagnosisId)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(diagnosisId);
            if (diagnosis == null) return NotFound();

            _context.Diagnoses.Remove(diagnosis);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
