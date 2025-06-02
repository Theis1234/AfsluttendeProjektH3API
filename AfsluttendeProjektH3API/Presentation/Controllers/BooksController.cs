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

namespace AfsluttendeProjektH3API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
		private readonly BookService _service;

		public BooksController(BookService service)
		{
			_service = service;
		}

		// GET: api/Books
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
			var books = await _service.GetAllAsync();
			return Ok(books);
		}

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
			var book = await _service.GetAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			return book;
		}

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
			if (id != book.Id)
			{
				return BadRequest();
			}

			await _service.UpdateAsync(book);
			return NoContent();
		}

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
			await _service.AddAsync(book);

			return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
		}

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
			await _service.DeleteAsync(id);
			return NoContent();
		}
    }
}
