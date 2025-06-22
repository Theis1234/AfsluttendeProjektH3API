namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class Artist
	{
		public int Id { get; init; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        public ContactInfo? ContactInfo { get; set; }
        public Address? Address { get; set; }
        public List<Award>? Awards { get; set; }
        public DateOnly DateOfBirth { get; set; }
		public List<ArtistCover> ArtistCovers { get; set; } = new List<ArtistCover>();
        public SocialLinks? SocialLinks { get; set; }
    }
}
