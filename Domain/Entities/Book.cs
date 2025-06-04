namespace AfsluttendeProjektH3API.Domain.Entities;
using System;

public class Book
{
	public int Id { get; init; }
	public string? Title { get; set; }
	public string? Genre { get; set; }
	public DateOnly PublishedDate { get; set; }
	public int NumberOfPages { get; set; }
	public double BasePrice { get; set; }
	public required Author Author { get; set; }
	public int AuthorId { get; set; }
}

