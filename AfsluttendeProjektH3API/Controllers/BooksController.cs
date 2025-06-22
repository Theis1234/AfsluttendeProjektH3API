using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AfsluttendeProjektH3API.Domain.Entities;
using AfsluttendeProjektH3API.Infrastructure;
using AfsluttendeProjektH3API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using AfsluttendeProjektH3API.Application.DTOs;
using Humanizer;

namespace AfsluttendeProjektH3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
		private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public BooksController(BookService service, AuthorService authorService)
        {
            _bookService = service;
            _authorService = authorService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string? title, string? genre, string? authorName)
        {
            var books = await _bookService.GetAllAsync(title, genre, authorName);
            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
			var book = await _bookService.GetAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			return book;
		}
        [HttpGet("by-author/{lastName}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthorLastName(string lastName)
        {
            var books = await _bookService.GetAllBooksByAuthorNameAsync(lastName);

            return Ok(books);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO bookDTO)
        {
			var book = await _bookService.GetAsync(id);

			if (book == null)
				return NotFound();

            book.Title = bookDTO.Title;
            book.AuthorId = bookDTO.AuthorId;
            book.GenreId = bookDTO.GenreId;
            book.PublishedDate = bookDTO.PublishedDate;
            book.NumberOfPages = bookDTO.NumberOfPages;
            book.BasePrice = bookDTO.BasePrice;

			await _bookService.UpdateAsync(book);
			return NoContent();
		}

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDTO)
        {
                var author = await _authorService.GetAsync(bookDTO.AuthorId);
            if (author == null)
                return BadRequest("Author not found");

            var book = new Book
            {
                Title = bookDTO.Title,
                PublishedDate = bookDTO.PublishedDate,
                NumberOfPages = bookDTO.NumberOfPages,
                GenreId = bookDTO.GenreId,
                BasePrice = bookDTO.BasePrice,
                AuthorId = bookDTO.AuthorId,
                Author = author,
                Editions = bookDTO.Editions
            };

            await _bookService.AddAsync(book);

			return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
		}

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
			await _bookService.DeleteAsync(id);
			return NoContent();
		}
    }
}
