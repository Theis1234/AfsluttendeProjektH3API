namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class Cover
	{
		public int Id { get; init; }
		public required string Title { get; set; }
		public bool DigitalOnly { get; set; }
		public Book Book { get; set; } = null!;
        public int BookId { get; set; }
		public List<ArtistCover> ArtistCovers { get; set; } = new List<ArtistCover>();
	}
}
