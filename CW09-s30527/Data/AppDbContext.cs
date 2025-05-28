using CW09_s30527.models;
using Microsoft.EntityFrameworkCore;

namespace CW09_s30527.Data;

public class AppDbContext: DbContext
{
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Medicament> Medicament { get; set; }
    public DbSet<Prescription> Prescription { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctors = new List<Doctor>
        {
            new()
            {
                IdDoctor = 1,
                FirstName = "Anna",
                LastName = "Nowicka",
                Email = "anna.nowicka@clinicmail.org"
            }
        };

        var patients = new List<Patient>
        {
            new()
            {
                IdPatient = 1,
                FirstName = "Marek",
                LastName = "Zieliński",
                Birthdate = new DateTime(1990, 5, 21, 10, 15, 0)
            }
        };

        var medicaments = new List<Medicament>
        {
            new()
            {
                IdMedicament = 1,
                Name = "Ibuprex",
                Description = "Lek przeciwbólowy i przeciwzapalny",
                Type = "Niesteroidowy lek przeciwzapalny"
            }
        };

        var prescriptions = new List<Prescription>
        {
            new()
            {
                IdPrescription = 1,
                Date = new DateTime(2025, 1, 10, 9, 0, 0),
                DueDate = new DateTime(2026, 1, 10, 9, 0, 0),
                IdPatient = 1,
                IdDoctor = 1
            }
        };

        var prescriptionMedicament = new List<Prescription_Medicament>
        {
            new()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 1,
                Details = "Zażywać po jedzeniu raz dziennie przez tydzień"
            }
        };

        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<Prescription_Medicament>().HasData(prescriptionMedicament);
    }
}