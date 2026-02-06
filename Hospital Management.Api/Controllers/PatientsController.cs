using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Management.Api.DTOs.Patients;
using Hospital_Management.Api.DTOs.Medical;
using Hospital_Management.Api.Filters;
using Hospital_Management.Api.Models.Patients;
using Hospital_Management.Api.Models.Medical;
using Hospital_Management.Api.Repositories.Interfaces;

namespace Hospital_Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [ApiResponseWrapper]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repo;

        public PatientsController(IPatientRepository repo)
        {
            _repo = repo;
        }

        // ───────────── PATIENTS ─────────────

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = (await _repo.GetAllPatientsAsync())
                .Select(p => new PatientDto
                {
                    Id = p.Id, FullName = p.FullName, DOB = p.DOB, Gender = p.Gender,
                    BloodGroup = p.BloodGroup, Allergies = p.Allergies,
                    ChronicConditions = p.ChronicConditions, Phone = p.Phone,
                    Email = p.Email, EmergencyContactName = p.EmergencyContactName,
                    EmergencyContactPhone = p.EmergencyContactPhone, EntryTime = p.EntryTime
                }).ToList();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var p = await _repo.GetPatientByIdAsync(id);
            if (p == null) return NotFound();

            return Ok(new PatientDto
            {
                Id = p.Id, FullName = p.FullName, DOB = p.DOB, Gender = p.Gender,
                BloodGroup = p.BloodGroup, Allergies = p.Allergies,
                ChronicConditions = p.ChronicConditions, Phone = p.Phone,
                Email = p.Email, EmergencyContactName = p.EmergencyContactName,
                EmergencyContactPhone = p.EmergencyContactPhone, EntryTime = p.EntryTime
            });
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

            await _repo.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, UpdatePatientDto dto)
        {
            var patient = await _repo.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            patient.FullName = dto.FullName; patient.DOB = dto.DOB; patient.Gender = dto.Gender;
            patient.BloodGroup = dto.BloodGroup; patient.Allergies = dto.Allergies;
            patient.ChronicConditions = dto.ChronicConditions; patient.Phone = dto.Phone;
            patient.Email = dto.Email; patient.EmergencyContactName = dto.EmergencyContactName;
            patient.EmergencyContactPhone = dto.EmergencyContactPhone;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var deleted = await _repo.DeletePatientAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── PATIENT ADDRESSES ─────────────

        [HttpGet("{patientId}/addresses")]
        public async Task<IActionResult> GetAddresses(int patientId)
        {
            var addresses = (await _repo.GetAddressesByPatientIdAsync(patientId))
                .Select(a => new PatientAddressDto
                {
                    Id = a.Id, PatientId = a.PatientId,
                    AddressLine = a.AddressLine, City = a.City, State = a.State
                }).ToList();

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

            await _repo.AddAddressAsync(address);
            return Created($"api/patients/{patientId}/addresses", new PatientAddressDto
            {
                Id = address.Id, PatientId = patientId,
                AddressLine = address.AddressLine, City = address.City, State = address.State
            });
        }

        [HttpPut("addresses/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, UpdatePatientAddressDto dto)
        {
            var address = await _repo.GetAddressByIdAsync(addressId);
            if (address == null) return NotFound();

            address.AddressLine = dto.AddressLine; address.City = dto.City; address.State = dto.State;

            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var deleted = await _repo.DeleteAddressAsync(addressId);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── MEDICAL RECORDS ─────────────

        [HttpGet("{patientId}/medical-records")]
        public async Task<IActionResult> GetMedicalRecords(int patientId)
        {
            var records = (await _repo.GetMedicalRecordsByPatientIdAsync(patientId))
                .Select(r => new MedicalRecordDto
                {
                    Id = r.Id, PatientId = r.PatientId, DoctorId = r.DoctorId,
                    Notes = r.Notes, CreatedAt = r.CreatedAt
                }).ToList();

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

            await _repo.AddMedicalRecordAsync(record);
            return Created($"api/patients/{patientId}/medical-records", new MedicalRecordDto
            {
                Id = record.Id, PatientId = patientId, DoctorId = record.DoctorId,
                Notes = record.Notes, CreatedAt = record.CreatedAt
            });
        }

        [HttpPut("medical-records/{recordId}")]
        public async Task<IActionResult> UpdateMedicalRecord(int recordId, UpdateMedicalRecordDto dto)
        {
            var record = await _repo.GetMedicalRecordByIdAsync(recordId);
            if (record == null) return NotFound();

            record.Notes = dto.Notes;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("medical-records/{recordId}")]
        public async Task<IActionResult> DeleteMedicalRecord(int recordId)
        {
            var deleted = await _repo.DeleteMedicalRecordAsync(recordId);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ───────────── DIAGNOSES ─────────────

        [HttpGet("medical-records/{recordId}/diagnoses")]
        public async Task<IActionResult> GetDiagnoses(int recordId)
        {
            var diagnoses = (await _repo.GetDiagnosesByRecordIdAsync(recordId))
                .Select(d => new DiagnosisDto
                {
                    Id = d.Id, MedicalRecordId = d.MedicalRecordId, Description = d.Description
                }).ToList();

            return Ok(diagnoses);
        }

        [HttpPost("medical-records/{recordId}/diagnoses")]
        public async Task<IActionResult> CreateDiagnosis(int recordId, CreateDiagnosisDto dto)
        {
            var diagnosis = new Diagnosis
            {
                MedicalRecordId = recordId, Description = dto.Description
            };

            await _repo.AddDiagnosisAsync(diagnosis);
            return Created($"api/patients/medical-records/{recordId}/diagnoses", dto);
        }

        [HttpPut("diagnoses/{diagnosisId}")]
        public async Task<IActionResult> UpdateDiagnosis(int diagnosisId, UpdateDiagnosisDto dto)
        {
            var diagnosis = await _repo.GetDiagnosisByIdAsync(diagnosisId);
            if (diagnosis == null) return NotFound();

            diagnosis.Description = dto.Description;
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("diagnoses/{diagnosisId}")]
        public async Task<IActionResult> DeleteDiagnosis(int diagnosisId)
        {
            var deleted = await _repo.DeleteDiagnosisAsync(diagnosisId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
