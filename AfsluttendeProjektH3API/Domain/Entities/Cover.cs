namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class Cover
	{
		public int Id { get; init; }
		public required string Title { get; set; }
		public bool DigitalOnly { get; set; }
		public Book? Book { get; set; }
		public int BookId { get; set; }
		public required List<Artist> Artists { get; set; } = new List<Artist>();
	}
}
