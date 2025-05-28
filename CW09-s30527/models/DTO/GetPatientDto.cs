namespace CW09_s30527.models.DTO;

public class GetPatientDto
{
    public int IdPatient { get; set; }
    public String FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public List<PrescriptionsDto> Prescriptions { get; set; } = null!;
}