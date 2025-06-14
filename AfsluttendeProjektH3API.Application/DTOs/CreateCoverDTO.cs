using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class CreateCoverDTO
	{
		public required string Title { get; set; }
		public bool DigitalOnly { get; set; }
		public int BookId { get; set; }
		public required List<int> ArtistIds = new List<int>();
	}
}
