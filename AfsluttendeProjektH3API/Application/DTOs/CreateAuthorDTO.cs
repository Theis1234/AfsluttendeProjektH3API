namespace AfsluttendeProjektH3API.Application.DTOs
{
	public class CreateAuthorDTO
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int NumberOfBooksPublished { get; set; }
		public string? LastPublishedBook { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public string? Nationality { get; set; }
	}
}
