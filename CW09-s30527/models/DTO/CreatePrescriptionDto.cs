namespace CW09_s30527.models.DTO;

public class CreatePrescriptionDto
{
    public PatientDto patient { get; set; } = null!;
    public List<CreatePrescriptionMedicamentDto> medicaments { get; set; } = null!;
    public DateTime Date {get; set;}
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}