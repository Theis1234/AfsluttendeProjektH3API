using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class AuthorDTO
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int NumberOfBooksPublished { get; set; }
		public string? LastPublishedBook { get; set; }
		public DateOnly DateOfBirth { get; set; }
        public int NationalityId { get; set; }
        public int PublisherId { get; set; }
        public Biography? Biography { get; set; }
        public Address? Address { get; set; }
        public ContactInfo? ContactInfo { get; set; }
        public int EducationId { get; set; }
    }
}
