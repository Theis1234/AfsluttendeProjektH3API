namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class ArtistCover
	{
		public Artist Artist { get; set; } = null!;
        public int ArtistId { get; set; }
		public Cover Cover { get; set; } = null!;
        public int CoverId { get; set; }
	}
}
