using Microsoft.EntityFrameworkCore;
using Hospital_Management.Api.Models.Admissions;
using Hospital_Management.Api.Models.Appointments;
using Hospital_Management.Api.Models.Auth;
using Hospital_Management.Api.Models.Billing;
using Hospital_Management.Api.Models.Doctors;
using Hospital_Management.Api.Models.Labs;
using Hospital_Management.Api.Models.Medical;
using Hospital_Management.Api.Models.Patients;
using Hospital_Management.Api.Models.Prescriptions;

namespace Hospital_Management.Api.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        // Patients
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddresses { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        // Doctors
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

        // Appointments
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

        // Admissions
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }

        // Billing
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

        // Labs
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

        // Medical
        public DbSet<Diagnosis> Diagnoses { get; set; }

        // Prescriptions
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        // Auth
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Patient → PatientAddress (one-to-many)
            modelBuilder.Entity<PatientAddress>()
                .HasOne(pa => pa.Patient)
                .WithMany(p => p.Addresses)
                .HasForeignKey(pa => pa.PatientId);

            // Patient → Appointment (one-to-many)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor → Appointment (one-to-many)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // AppointmentStatus → Appointment (one-to-many)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Status)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StatusId);

            // Department → Doctor (one-to-many)
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                .HasForeignKey(d => d.DepartmentId);

            // DoctorSpecialization → Doctor (one-to-many)
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId);

            // Patient → MedicalRecord (one-to-many)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor → MedicalRecord (one-to-many)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Doctor)
                .WithMany(d => d.MedicalRecords)
                .HasForeignKey(mr => mr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // MedicalRecord → Diagnosis (one-to-many)
            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.MedicalRecord)
                .WithMany(mr => mr.Diagnoses)
                .HasForeignKey(d => d.MedicalRecordId);

            // Patient → Admission (one-to-many)
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Admissions)
                .HasForeignKey(a => a.PatientId);

            // Admission → Discharge (one-to-one)
            modelBuilder.Entity<Discharge>()
                .HasOne(d => d.Admission)
                .WithOne(a => a.Discharge)
                .HasForeignKey<Discharge>(d => d.AdmissionId);

            // Patient → Discharge (one-to-many)
            modelBuilder.Entity<Discharge>()
                .HasOne(d => d.Patient)
                .WithMany(p => p.Discharges)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient → Bill (one-to-many)
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PatientId);

            // Bill → BillItem (one-to-many)
            modelBuilder.Entity<BillItem>()
                .HasOne(bi => bi.Bill)
                .WithMany(b => b.BillItems)
                .HasForeignKey(bi => bi.BillId);

            // Bill → InsuranceClaim (one-to-one)
            modelBuilder.Entity<InsuranceClaim>()
                .HasOne(ic => ic.Bill)
                .WithOne(b => b.InsuranceClaim)
                .HasForeignKey<InsuranceClaim>(ic => ic.BillId);

            // Patient → LabResult (one-to-many)
            modelBuilder.Entity<LabResult>()
                .HasOne(lr => lr.Patient)
                .WithMany(p => p.LabResults)
                .HasForeignKey(lr => lr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // LabTest → LabResult (one-to-many)
            modelBuilder.Entity<LabResult>()
                .HasOne(lr => lr.LabTest)
                .WithMany(lt => lt.LabResults)
                .HasForeignKey(lr => lr.LabTestId);

            // Patient → Prescription (one-to-many)
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(pt => pt.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor → Prescription (one-to-many)
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prescription → PrescriptionItem (one-to-many)
            modelBuilder.Entity<PrescriptionItem>()
                .HasOne(pi => pi.Prescription)
                .WithMany(p => p.PrescriptionItems)
                .HasForeignKey(pi => pi.PrescriptionId);

            // Medicine → PrescriptionItem (one-to-many)
            modelBuilder.Entity<PrescriptionItem>()
                .HasOne(pi => pi.Medicine)
                .WithMany(m => m.PrescriptionItems)
                .HasForeignKey(pi => pi.MedicineId);

            // Decimal precision for money columns
            modelBuilder.Entity<Bill>()
                .Property(b => b.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<BillItem>()
                .Property(bi => bi.Amount)
                .HasColumnType("decimal(18,2)");

            // User – unique email index
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
