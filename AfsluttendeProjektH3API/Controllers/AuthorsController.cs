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
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
			var authors = await _service.GetAllAsync();
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
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
			await _service.AddAsync(author);

			return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
		}

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
			await _service.DeleteAsync(id);
			return NoContent();
		}
    }
}
