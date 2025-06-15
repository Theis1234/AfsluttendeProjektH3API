using System.Text.Json.Serialization;

namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class ArtistCover
	{
		[JsonIgnore]
		public Artist Artist { get; set; } = null!;
        public int ArtistId { get; set; }
        [JsonIgnore]
        public Cover Cover { get; set; } = null!;
        public int CoverId { get; set; }
	}
}
