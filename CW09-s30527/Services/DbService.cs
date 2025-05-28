using CW09_s30527.Data;
using CW09_s30527.models;
using CW09_s30527.models.DTO;

using Microsoft.EntityFrameworkCore;
namespace CW09_s30527.Services;

public interface IDbService
{
    public Task<GetPatientDto> GetPatient(int patient);
    public Task CreatePrescription(CreatePrescriptionDto dto);

}

public class DbService(AppDbContext data) : IDbService
    {
        public async Task<GetPatientDto> GetPatient(int patientid)
        {
  
            var result = await data.Patient.AsQueryable().Where(pt => pt.IdPatient == patientid).Select(pt =>
                new GetPatientDto
                {
                    IdPatient = pt.IdPatient,
                    FirstName = pt.FirstName,
                    LastName = pt.LastName,
                    Birthdate = pt.Birthdate,
                    Prescriptions = pt.Prescriptions.OrderByDescending(pr => pr.DueDate).Select(pr =>
                        new PrescriptionsDto
                        {
                            IdPrescription = pr.IdPrescription,
                            Date = pr.Date,
                            DueDate = pr.DueDate,
                            Medicaments = pr.prescriptionMedicaments.Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Description = pm.Medicament.Description,
                                Type = pm.Medicament.Type
                            }).ToList(),
                            Doctor = new DoctorSimpleDto
                            {
                                IdDoctor = pr.Doctor.IdDoctor,
                                FirstName = pr.Doctor.FirstName,
                            }
                        }).ToList()
                }).FirstOrDefaultAsync();
            if (result != null)
            {
                return result;
            }

            throw new Exception($"Patient with ID {patientid} not found");
        }

        public async Task CreatePrescription(CreatePrescriptionDto dto)
        {

            if (dto.DueDate < dto.Date)
                throw new Exception("DueDate must be after or equal to Date.");

            if (dto.medicaments.Count > 10)
                throw new Exception("A prescription cannot contain more than 10 medicaments.");


            var patient = await data.Patient.FindAsync(dto.patient.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = dto.patient.FirstName,
                    LastName = dto.patient.LastName,
                    Birthdate = dto.patient.DateOfBirth
                };
                data.Patient.Add(patient);
                await data.SaveChangesAsync();
            }


            var doctor = await data.Doctor.FindAsync(dto.IdDoctor);
            if (doctor == null)
                throw new Exception($"Doctor with ID {dto.IdDoctor} not found.");


            var medicamentIds = dto.medicaments.Select(m => m.IdMedicament).ToList();

            var medicamentEntities = await data.Medicament
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .ToListAsync();

            if (medicamentEntities.Count != dto.medicaments.Count)
                throw new Exception("One or more medicaments were not found.");


            var prescription = new Prescription
            {
                Date = dto.Date,
                DueDate = dto.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = doctor.IdDoctor,
            };

            data.Prescription.Add(prescription);
            foreach (var medicamentId in dto.medicaments)
            {
                var prescriptionMedicament = new Prescription_Medicament()
                {
                    IdMedicament = medicamentId.IdMedicament,
                    IdPrescription = prescription.IdPrescription,
                    Dose = medicamentId.Dose,
                    Details = medicamentId.Details,
                };
                data.Prescription_Medicament.Add(prescriptionMedicament);
            }
            await data.SaveChangesAsync();
            
        }
    }
