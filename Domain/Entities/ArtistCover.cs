namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class ArtistCover
	{
		public required Artist Artist { get; set; }
		public int ArtistId { get; set; }
		public required Cover Cover { get; set; }
		public int CoverId { get; set; }
	}
}
