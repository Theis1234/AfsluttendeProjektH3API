namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class ArtistCover
	{
		public required Book Book { get; set; }
		public int BookId { get; set; }
		public required Cover Cover { get; set; }
		public int CoverId { get; set; }
	}
}
