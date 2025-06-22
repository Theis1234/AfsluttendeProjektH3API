using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class ArtistDTO
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
        public int NationalityId { get; set; }
        public ContactInfo? ContactInfo { get; set; }
        public Address? Address { get; set; }
		public List<AwardDTO>? Awards { get; set; }
        public SocialLinks? SocialLinks { get; set; }
        public DateOnly DateOfBirth { get; set; }
	}
}
