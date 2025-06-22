using System.Text.Json.Serialization;

namespace AfsluttendeProjektH3API.Application;

public class PublisherDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ContactEmail { get; set; }
}
