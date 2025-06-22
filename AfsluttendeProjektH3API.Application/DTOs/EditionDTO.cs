namespace AfsluttendeProjektH3API.Application;

public class EditionDTO
{
    public string Format { get; set; } = "";
    public DateOnly ReleaseDate { get; set; }
    public int BookId { get; set; }
}
