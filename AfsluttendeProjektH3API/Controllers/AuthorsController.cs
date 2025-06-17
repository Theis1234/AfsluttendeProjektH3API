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

namespace AfsluttendeProjektH3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
		private readonly AuthorService _service;

		public AuthorsController(AuthorService service)
		{
			_service = service;
		}

		// GET: api/Authors
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors(string? firstName, string? lastName, string? nationality)
        {
            var authors = await _service.GetAllAsync(firstName, lastName, nationality);
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
			var author = await _service.GetAsync(id);

			if (author == null)
			{
				return NotFound();
			}

			return author;
		}

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
			if (id != author.Id)
			{
				return BadRequest();
			}

			await _service.UpdateAsync(author);
			return NoContent();
		}

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDTO authorDTO)
        {
            var author = new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
                Nationality = authorDTO.Nationality,
                NumberOfBooksPublished = authorDTO.NumberOfBooksPublished,
                DateOfBirth = authorDTO.DateOfBirth,
                LastPublishedBook = authorDTO.LastPublishedBook
            };


            await _service.AddAsync(author);

			return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
		}

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
			await _service.DeleteAsync(id);
			return NoContent();
		}
    }
}
