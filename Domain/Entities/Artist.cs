namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class Artist
	{
		public int Id { get; init; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Nationality { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public required List<ArtistCover> ArtistCovers { get; set; } = new List<ArtistCover>();
	}
}
