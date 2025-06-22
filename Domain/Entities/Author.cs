namespace AfsluttendeProjektH3API.Domain.Entities
{
	public class Author
	{
		public int Id {	get; init; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int NumberOfBooksPublished { get; set; }
		public string? LastPublishedBook { get; set; }
		public DateOnly DateOfBirth { get; set; }
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
		public Biography? Biography { get; set; }
        public Address? Address { get; set; }
        public ContactInfo? ContactInfo { get; set; }
		public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public int EducationId { get; set; }
        public Education? Education { get; set; }
    }
}
