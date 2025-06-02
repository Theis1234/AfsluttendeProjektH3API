using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AfsluttendeProjektH3API.Domain.Entities;
using AfsluttendeProjektH3API.Infrastructure;

namespace AfsluttendeProjektH3API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoversController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoversController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Covers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cover>>> GetCovers()
        {
            return await _context.Covers.ToListAsync();
        }

        // GET: api/Covers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cover>> GetCover(int id)
        {
            var cover = await _context.Covers.FindAsync(id);

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

            _context.Entry(cover).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Covers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cover>> PostCover(Cover cover)
        {
            _context.Covers.Add(cover);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCover", new { id = cover.Id }, cover);
        }

        // DELETE: api/Covers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCover(int id)
        {
            var cover = await _context.Covers.FindAsync(id);
            if (cover == null)
            {
                return NotFound();
            }

            _context.Covers.Remove(cover);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoverExists(int id)
        {
            return _context.Covers.Any(e => e.Id == id);
        }
    }
}
