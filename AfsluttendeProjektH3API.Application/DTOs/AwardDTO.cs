using System.Text.Json.Serialization;

namespace AfsluttendeProjektH3API.Application;

public class AwardDTO
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateReceived { get; set; }
    public string? Description { get; set; }
    public int ArtistId { get; set; }
}
