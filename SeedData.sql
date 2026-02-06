-- ======================================
-- Hospital Management Seed Data
-- Insert order: Master data → Parents → Children
-- ======================================

USE HospitalManagementDb;
GO

-- Clear all existing data (child tables first)
DELETE FROM LabResults;
DELETE FROM PrescriptionItems;
DELETE FROM Prescriptions;
DELETE FROM InsuranceClaims;
DELETE FROM BillItems;
DELETE FROM Bills;
DELETE FROM Diagnoses;
DELETE FROM MedicalRecords;
DELETE FROM Discharges;
DELETE FROM Admissions;
DELETE FROM Appointments;
DELETE FROM Doctors;
DELETE FROM PatientAddresses;
DELETE FROM Patients;
DELETE FROM LabTests;
DELETE FROM Medicines;
DELETE FROM AppointmentStatuses;
DELETE FROM DoctorSpecializations;
DELETE FROM Departments;
GO

-- ───────────── DEPARTMENTS ─────────────
SET IDENTITY_INSERT Departments ON;
INSERT INTO Departments (Id, Name) VALUES
(1, 'Cardiology'),
(2, 'Neurology'),
(3, 'Orthopedics'),
(4, 'Pediatrics'),
(5, 'General Medicine');
SET IDENTITY_INSERT Departments OFF;

-- ───────────── DOCTOR SPECIALIZATIONS ─────────────
SET IDENTITY_INSERT DoctorSpecializations ON;
INSERT INTO DoctorSpecializations (Id, Name) VALUES
(1, 'Cardiologist'),
(2, 'Neurologist'),
(3, 'Orthopedic Surgeon'),
(4, 'Pediatrician'),
(5, 'General Physician');
SET IDENTITY_INSERT DoctorSpecializations OFF;

-- ───────────── APPOINTMENT STATUSES ─────────────
SET IDENTITY_INSERT AppointmentStatuses ON;
INSERT INTO AppointmentStatuses (Id, Name) VALUES
(1, 'Scheduled'),
(2, 'Completed'),
(3, 'Cancelled'),
(4, 'No Show');
SET IDENTITY_INSERT AppointmentStatuses OFF;

-- ───────────── MEDICINES ─────────────
SET IDENTITY_INSERT Medicines ON;
INSERT INTO Medicines (Id, Name) VALUES
(1, 'Paracetamol'),
(2, 'Amoxicillin'),
(3, 'Ibuprofen'),
(4, 'Metformin'),
(5, 'Aspirin'),
(6, 'Omeprazole'),
(7, 'Cetirizine');
SET IDENTITY_INSERT Medicines OFF;

-- ───────────── LAB TESTS ─────────────
SET IDENTITY_INSERT LabTests ON;
INSERT INTO LabTests (Id, Name) VALUES
(1, 'Complete Blood Count (CBC)'),
(2, 'Blood Sugar (Fasting)'),
(3, 'Lipid Profile'),
(4, 'Thyroid Panel (TSH)'),
(5, 'Liver Function Test (LFT)'),
(6, 'Kidney Function Test (KFT)'),
(7, 'Urine Routine');
SET IDENTITY_INSERT LabTests OFF;

-- ───────────── PATIENTS ─────────────
SET IDENTITY_INSERT Patients ON;
INSERT INTO Patients (Id, FullName, DOB, Gender, BloodGroup, Allergies, ChronicConditions, Phone, Email, EmergencyContactName, EmergencyContactPhone, EntryTime) VALUES
(1, 'Rahul Sharma',     '1990-05-15', 'Male',   'B+',  'Dust',          'None',          '9876543210', 'rahul@email.com',    'Priya Sharma',    '9876543211', '2026-01-10 09:00:00'),
(2, 'Ananya Patel',     '1985-08-22', 'Female', 'O+',  'Penicillin',    'Diabetes',      '9876543220', 'ananya@email.com',   'Vikram Patel',    '9876543221', '2026-01-12 10:30:00'),
(3, 'Arjun Singh',      '2000-03-10', 'Male',   'A+',  'None',          'Asthma',        '9876543230', 'arjun@email.com',    'Meera Singh',     '9876543231', '2026-01-15 08:15:00'),
(4, 'Sneha Gupta',      '1978-11-28', 'Female', 'AB-', 'Sulfa drugs',   'Hypertension',  '9876543240', 'sneha@email.com',    'Rajesh Gupta',    '9876543241', '2026-01-18 11:00:00'),
(5, 'Kabir Verma',      '1995-07-05', 'Male',   'O-',  'None',          'None',          '9876543250', 'kabir@email.com',    'Suman Verma',     '9876543251', '2026-01-20 14:30:00');
SET IDENTITY_INSERT Patients OFF;

-- ───────────── PATIENT ADDRESSES ─────────────
SET IDENTITY_INSERT PatientAddresses ON;
INSERT INTO PatientAddresses (Id, PatientId, AddressLine, City, State) VALUES
(1, 1, '12 MG Road, Sector 5',         'Mumbai',     'Maharashtra'),
(2, 2, '45 Park Street',               'Kolkata',    'West Bengal'),
(3, 3, '78 Rajpath Nagar',             'Delhi',      'Delhi'),
(4, 4, '23 Jubilee Hills',             'Hyderabad',  'Telangana'),
(5, 5, '56 Koramangala 4th Block',     'Bangalore',  'Karnataka'),
(6, 1, '99 Marine Drive, Flat 302',    'Mumbai',     'Maharashtra');
SET IDENTITY_INSERT PatientAddresses OFF;

-- ───────────── DOCTORS ─────────────
SET IDENTITY_INSERT Doctors ON;
INSERT INTO Doctors (Id, FullName, DepartmentId, SpecializationId) VALUES
(1, 'Dr. Anil Kumar',      1, 1),
(2, 'Dr. Sunita Reddy',    2, 2),
(3, 'Dr. Manoj Tiwari',    3, 3),
(4, 'Dr. Pooja Mehta',     4, 4),
(5, 'Dr. Ravi Shankar',    5, 5);
SET IDENTITY_INSERT Doctors OFF;

-- ───────────── APPOINTMENTS ─────────────
SET IDENTITY_INSERT Appointments ON;
INSERT INTO Appointments (Id, PatientId, DoctorId, AppointmentDate, StatusId) VALUES
(1, 1, 1, '2026-02-01 10:00:00', 2),
(2, 2, 5, '2026-02-02 11:00:00', 2),
(3, 3, 3, '2026-02-03 09:30:00', 1),
(4, 4, 2, '2026-02-04 14:00:00', 1),
(5, 5, 4, '2026-02-05 16:00:00', 3),
(6, 1, 5, '2026-02-07 10:00:00', 1),
(7, 2, 1, '2026-02-08 11:30:00', 1);
SET IDENTITY_INSERT Appointments OFF;

-- ───────────── ADMISSIONS ─────────────
SET IDENTITY_INSERT Admissions ON;
INSERT INTO Admissions (Id, PatientId, AdmissionDate) VALUES
(1, 1, '2026-01-25'),
(2, 2, '2026-01-28'),
(3, 4, '2026-02-01');
SET IDENTITY_INSERT Admissions OFF;

-- ───────────── DISCHARGES ─────────────
SET IDENTITY_INSERT Discharges ON;
INSERT INTO Discharges (Id, PatientId, AdmissionId, DischargeDate, DischargeNotes) VALUES
(1, 1, 1, '2026-01-30', 'Patient recovered well. Follow up in 2 weeks.'),
(2, 2, 2, '2026-02-03', 'Diabetes under control. Continue medication.');
SET IDENTITY_INSERT Discharges OFF;

-- ───────────── MEDICAL RECORDS ─────────────
SET IDENTITY_INSERT MedicalRecords ON;
INSERT INTO MedicalRecords (Id, PatientId, DoctorId, Notes, CreatedAt) VALUES
(1, 1, 1, 'Patient presented with chest pain. ECG normal. Advised stress test.',     '2026-02-01'),
(2, 2, 5, 'Routine diabetic checkup. Blood sugar slightly elevated.',                '2026-02-02'),
(3, 3, 3, 'Patient has knee pain from sports injury. MRI recommended.',              '2026-02-03'),
(4, 4, 2, 'Frequent headaches and dizziness. CT scan advised.',                      '2026-02-04');
SET IDENTITY_INSERT MedicalRecords OFF;

-- ───────────── DIAGNOSES ─────────────
SET IDENTITY_INSERT Diagnoses ON;
INSERT INTO Diagnoses (Id, MedicalRecordId, Description) VALUES
(1, 1, 'Mild angina – stress-related'),
(2, 2, 'Type 2 Diabetes Mellitus – uncontrolled'),
(3, 3, 'Anterior Cruciate Ligament (ACL) tear – Grade 2'),
(4, 4, 'Migraine with aura'),
(5, 2, 'Vitamin D deficiency');
SET IDENTITY_INSERT Diagnoses OFF;

-- ───────────── BILLS ─────────────
SET IDENTITY_INSERT Bills ON;
INSERT INTO Bills (Id, PatientId, TotalAmount, IsPaid) VALUES
(1, 1, 15000.00, 1),
(2, 2, 8500.50,  1),
(3, 3, 3200.00,  0),
(4, 4, 22000.00, 0);
SET IDENTITY_INSERT Bills OFF;

-- ───────────── BILL ITEMS ─────────────
SET IDENTITY_INSERT BillItems ON;
INSERT INTO BillItems (Id, BillId, Description, Amount) VALUES
(1, 1, 'Consultation Fee',    1500.00),
(2, 1, 'ECG Test',            3000.00),
(3, 1, 'Room Charges (5 days)', 10000.00),
(4, 1, 'Medicines',           500.00),
(5, 2, 'Consultation Fee',    1000.00),
(6, 2, 'Blood Tests',         3500.00),
(7, 2, 'Medicines',           4000.50),
(8, 3, 'Consultation Fee',    1200.00),
(9, 3, 'X-Ray',               2000.00),
(10, 4, 'Consultation Fee',   2000.00),
(11, 4, 'CT Scan',            12000.00),
(12, 4, 'Room Charges (3 days)', 8000.00);
SET IDENTITY_INSERT BillItems OFF;

-- ───────────── INSURANCE CLAIMS ─────────────
SET IDENTITY_INSERT InsuranceClaims ON;
INSERT INTO InsuranceClaims (Id, BillId, ProviderName, Status) VALUES
(1, 1, 'Star Health Insurance',    'Approved'),
(2, 4, 'ICICI Lombard',            'Pending');
SET IDENTITY_INSERT InsuranceClaims OFF;

-- ───────────── PRESCRIPTIONS ─────────────
SET IDENTITY_INSERT Prescriptions ON;
INSERT INTO Prescriptions (Id, PatientId, DoctorId, DateIssued) VALUES
(1, 1, 1, '2026-02-01'),
(2, 2, 5, '2026-02-02'),
(3, 3, 3, '2026-02-03'),
(4, 4, 2, '2026-02-04');
SET IDENTITY_INSERT Prescriptions OFF;

-- ───────────── PRESCRIPTION ITEMS ─────────────
SET IDENTITY_INSERT PrescriptionItems ON;
INSERT INTO PrescriptionItems (Id, PrescriptionId, MedicineId, Dosage, DurationDays) VALUES
(1, 1, 5, '75mg once daily after breakfast',     30),
(2, 1, 6, '20mg once daily before dinner',       14),
(3, 2, 4, '500mg twice daily after meals',       60),
(4, 2, 1, '500mg as needed for fever',           5),
(5, 3, 3, '400mg twice daily after meals',       10),
(6, 4, 7, '10mg once daily at bedtime',          15),
(7, 4, 1, '650mg as needed for headache',        7);
SET IDENTITY_INSERT PrescriptionItems OFF;

-- ───────────── LAB RESULTS ─────────────
SET IDENTITY_INSERT LabResults ON;
INSERT INTO LabResults (Id, PatientId, LabTestId, Result, TestDate) VALUES
(1, 1, 1, 'Hb: 14.2 g/dL, WBC: 7500, Platelets: 250000 – Normal',              '2026-02-01'),
(2, 2, 2, 'Fasting Blood Sugar: 185 mg/dL – High',                               '2026-02-02'),
(3, 2, 3, 'Total Cholesterol: 220, LDL: 145, HDL: 42 – Borderline High',        '2026-02-02'),
(4, 3, 1, 'Hb: 15.0 g/dL, WBC: 8200, Platelets: 280000 – Normal',              '2026-02-03'),
(5, 4, 4, 'TSH: 2.8 mIU/L – Normal',                                            '2026-02-04'),
(6, 2, 5, 'SGPT: 32, SGOT: 28, Bilirubin: 0.9 – Normal',                       '2026-02-02');
SET IDENTITY_INSERT LabResults OFF;
 
PRINT '✅ Seed data inserted successfully!';
GO
