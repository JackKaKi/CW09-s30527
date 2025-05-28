namespace CW09_s30527.models.DTO;

public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!; 
    public DateTime DateOfBirth { get; set; }
}