using System.Text.Json.Serialization;

namespace AfsluttendeProjektH3API.Application;

public class EducationDTO
{
    public int Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
}
