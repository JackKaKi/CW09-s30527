namespace CW09_s30527.models.DTO;

public class CreatePrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; } = null!;
}