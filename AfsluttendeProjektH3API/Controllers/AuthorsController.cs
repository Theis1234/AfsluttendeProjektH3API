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
using AfsluttendeProjektH3API.Application.Interfaces;

namespace AfsluttendeProjektH3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
		private readonly IAuthorService _service;

		public AuthorsController(IAuthorService service)
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
        [HttpGet("lastName/{lastName}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsByLastName(string lastName)
        {
            var artists = await _service.GetAllAuthorsByLastNameAsync(lastName);

            return Ok(artists);
        }
        [HttpGet("firstName/{firstName}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsByFirstName(string firstName)
        {
            var artists = await _service.GetAllAuthorsByFirstNameAsync(firstName);

            return Ok(artists);
        }
        [HttpGet("nationality/{nationality}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorssByNationality(string nationality)
        {
            var artists = await _service.GetAllAuthorsByNationalityAsync(nationality);

            return Ok(artists);
        }


        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDTO)
        {
            var author = await _service.GetAsync(id);

            if (author == null)
                return NotFound();

            author.FirstName = authorDTO.FirstName;
            author.LastName = authorDTO.LastName;
            author.DateOfBirth = authorDTO.DateOfBirth;
            author.NumberOfBooksPublished = authorDTO.NumberOfBooksPublished;
            author.LastPublishedBook = authorDTO.LastPublishedBook;
            author.Address = authorDTO.Address;
            author.Biography = authorDTO.Biography;
            author.ContactInfo = authorDTO.ContactInfo;
            author.EducationId = authorDTO.EducationId;
            author.PublisherId = authorDTO.PublisherId;
            author.NationalityId = authorDTO.NationalityId;

            await _service.UpdateAsync(author);
			return NoContent();
		}

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
                NumberOfBooksPublished = authorDTO.NumberOfBooksPublished,
                DateOfBirth = authorDTO.DateOfBirth,
                LastPublishedBook = authorDTO.LastPublishedBook,
                Address = authorDTO.Address,
                Biography = authorDTO.Biography,
                ContactInfo = authorDTO.ContactInfo,
                EducationId = authorDTO.EducationId,
                PublisherId = authorDTO.PublisherId,
                NationalityId = authorDTO.NationalityId
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
