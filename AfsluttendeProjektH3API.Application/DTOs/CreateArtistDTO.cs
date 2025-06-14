using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class CreateArtistDTO
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Nationality { get; set; }
		public DateOnly DateOfBirth { get; set; }
	}
}
