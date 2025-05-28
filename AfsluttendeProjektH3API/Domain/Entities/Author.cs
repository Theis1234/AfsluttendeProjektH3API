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
		public string? Nationality { get; set; }
	}
}
