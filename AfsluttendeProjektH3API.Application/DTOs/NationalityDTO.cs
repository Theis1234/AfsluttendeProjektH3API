using System.Text.Json.Serialization;

namespace AfsluttendeProjektH3API.Application;

public class NationalityDTO
{
    public int Id { get; set; }
    public string CountryCode { get; set; } = "Undefined";
    public string CountryName { get; set; } = "Undefined";
}
