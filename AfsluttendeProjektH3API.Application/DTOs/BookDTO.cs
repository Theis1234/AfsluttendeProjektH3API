using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class BookDTO
	{
		public string? Title { get; set; }
		public int GenreId { get; set; }
		public DateOnly PublishedDate { get; set; }
		public int NumberOfPages { get; set; }
		public double BasePrice { get; set; }
		public int AuthorId { get; set; }
		public List<Edition> Editions { get; set; } = new();
    }
}
