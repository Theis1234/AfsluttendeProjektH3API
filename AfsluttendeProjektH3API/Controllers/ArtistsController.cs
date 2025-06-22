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
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _service;

        public ArtistsController(IArtistService service)
        {
            _service = service;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists(string? firstName, string? lastName, string? nationality)
        {
            var artists =  await _service.GetAllAsync(firstName, lastName, nationality);
            return Ok(artists);
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _service.GetAsync(id);
             
            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        [HttpGet("lastName/{lastName}")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsByLastName(string lastName)
        {
            var artists = await _service.GetAllArtistsByLastNameAsync(lastName);

            return Ok(artists);
        }
        [HttpGet("firstName/{firstName}")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsByFirstName(string firstName)
        {
            var artists = await _service.GetAllArtistsByFirstNameAsync(firstName);

            return Ok(artists);
        }
        [HttpGet("nationality/{nationality}")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsByNationality(string nationality)
        {
            var artists = await _service.GetAllArtistsByNationalityAsync(nationality);

            return Ok(artists);
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutArtist(int id, ArtistDTO artistDTO)
        {
            var artist = await _service.GetAsync(id);

            if (artist == null)
                return NotFound();

            artist.FirstName = artistDTO.FirstName;
            artist.LastName = artistDTO.LastName;
            artist.DateOfBirth = artistDTO.DateOfBirth;
            artist.NationalityId = artistDTO.NationalityId;
            artist.Address = artistDTO.Address;
            artist.ContactInfo = artistDTO.ContactInfo;
            artist.Awards = artistDTO.Awards?.Select(a => new Award
            {
                Name = a.Name,
                DateReceived = a.DateReceived,
                Description = a.Description,

            }).ToList();
                artist.SocialLinks = artistDTO.SocialLinks;

            await _service.UpdateAsync(artist);
            return NoContent();
        }

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Artist>> PostArtist(ArtistDTO artistDTO)
        {
            var artist = new Artist
            {
                FirstName = artistDTO.FirstName,
                LastName = artistDTO.LastName,
                DateOfBirth = artistDTO.DateOfBirth,
                NationalityId = artistDTO.NationalityId,
                Address = artistDTO.Address,
                ContactInfo = artistDTO.ContactInfo,
                Awards = artistDTO.Awards?.Select(a => new Award
                {
                    Name = a.Name,
                    DateReceived = a.DateReceived,
                    Description = a.Description,
                    
                }).ToList(),
                SocialLinks = artistDTO.SocialLinks
                
            };


            await _service.AddAsync(artist);

            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
