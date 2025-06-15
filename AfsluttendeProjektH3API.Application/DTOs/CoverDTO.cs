using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class CoverDTO
	{
		public required string Title { get; set; }
		public bool DigitalOnly { get; set; }
		public int BookId { get; set; }
		public List<int> ArtistIds { get; set; } = new List<int>();
	}
}
