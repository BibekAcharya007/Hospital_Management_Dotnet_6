using System;

namespace Hospital_Management.Api.Models.Admissions
{
    public class Admission
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public DateTime AdmissionDate { get; set; }
    }
}
