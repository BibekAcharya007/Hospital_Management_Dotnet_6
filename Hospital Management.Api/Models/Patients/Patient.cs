using System;
using System.Collections.Generic;
namespace Hospital_Management.Api.Models.Patients
{
    public class Patient
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? Allergies { get; set; }
        public string? ChronicConditions { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyConctactPhone { get; set; }
        public DateTime EntryTime { get; set}
        public ICollection<PatientAddress> Address { get; set; }
    }
}
