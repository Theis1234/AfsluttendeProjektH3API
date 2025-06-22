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
using AfsluttendeProjektH3API.Application.Interfaces;

namespace AfsluttendeProjektH3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoversController : ControllerBase
    {
		private readonly ICoverService _service;

		public CoversController(ICoverService service)
		{
			_service = service;
		}

		// GET: api/Covers
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Cover>>> GetCovers(string? title, bool? digitalOnly)
        {
			var covers = await _service.GetAllAsync(title, digitalOnly);
			return Ok(covers);
		}

        // GET: api/Covers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cover>> GetCover(int id)
        {
			var cover = await _service.GetAsync(id);

			if (cover == null)
			{
				return NotFound();
			}

			return cover;
		}
        [HttpGet("book/{id}")]
        public async Task<ActionResult<Cover>> GetCoverByBookId(int id)
        {
            var cover = await _service.GetCoverByBookIdAsync(id);

            if (cover == null)
            {
                return NotFound();
            }

            return cover;
        }

        [HttpGet("artist/{id}")]
        public async Task<ActionResult<IEnumerable<Cover>>> GetCoversByArtist(int id)
        {
            var covers = await _service.GetCoversByArtistAsync(id);

            if (covers == null)
            {
                return NotFound();
            }

            return Ok(covers);
        }
        [HttpGet("digitalOnly/{digitalOnly}")]
        public async Task<ActionResult<IEnumerable<Cover>>> GetDigitalOnlyCovers(bool digitalOnly)
        {
            var covers = await _service.GetDigitalOnlyCoversAsync(digitalOnly);

            if (covers == null)
            {
                return NotFound();
            }

            return Ok(covers);
        }
        // PUT: api/Covers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCover(int id, CoverDTO coverDTO)
        {
            var cover = await _service.GetAsync(id);

            if (cover == null)
                return NotFound();

            cover.Title = coverDTO.Title;
            cover.DigitalOnly = coverDTO.DigitalOnly;
            cover.BookId = coverDTO.BookId;

            cover.ArtistCovers.Clear();
            foreach (var artistId in coverDTO.ArtistIds)
            {
                cover.ArtistCovers.Add(new ArtistCover
                {
                    ArtistId = artistId,
                    CoverId = cover.Id
                });               
            }
            await _service.UpdateAsync(cover);
            return NoContent();
        }
        // POST: api/Covers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Cover>> PostCover(CoverDTO coverDTO)
        {
            try
            {
                var cover = new Cover
                {
                    Title = coverDTO.Title,
                    DigitalOnly = coverDTO.DigitalOnly,
                    BookId = coverDTO.BookId
                };
                foreach (var artistId in coverDTO.ArtistIds)
                {
                    var artistCover = new ArtistCover
                    {
                        ArtistId = artistId,
                        Cover = cover
                    };

                    cover.ArtistCovers.Add(artistCover);
                }

                await _service.AddAsync(cover);

                return CreatedAtAction(nameof(GetCover), new { id = cover.Id }, cover);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString());
            }
          
		}

        // DELETE: api/Covers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCover(int id)
        {
			await _service.DeleteAsync(id);
			return NoContent();
		}
    }
}
