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
    public class CoversController : ControllerBase
    {
		private readonly CoverService _service;

		public CoversController(CoverService service)
		{
			_service = service;
		}

		// GET: api/Covers
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Cover>>> GetCovers()
        {
			var covers = await _service.GetAllAsync();
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

        // PUT: api/Covers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCover(int id, Cover cover)
        {
			if (id != cover.Id)
			{
				return BadRequest();
			}

			await _service.UpdateAsync(cover);
			return NoContent();
		}

        // POST: api/Covers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cover>> PostCover(Cover cover)
        {
			await _service.AddAsync(cover);

			return CreatedAtAction(nameof(GetCover), new { id = cover.Id }, cover);
		}

        // DELETE: api/Covers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCover(int id)
        {
			await _service.DeleteAsync(id);
			return NoContent();
		}
    }
}
