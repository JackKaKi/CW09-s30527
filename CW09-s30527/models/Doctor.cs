using System.ComponentModel.DataAnnotations;

namespace CW09_s30527.models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [MaxLength(100)] public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(100)]
    public string Email { get; set; } = null!;
    
    public virtual ICollection<Prescription> Prescriptions { get; set; } = null!;
}