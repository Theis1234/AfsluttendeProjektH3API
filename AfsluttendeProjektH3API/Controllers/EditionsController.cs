using AfsluttendeProjektH3API.Application;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace AfsluttendeProjektH3API;

[Route("api/[controller]")]
[ApiController]
public class EditionsController : ControllerBase
{
    private readonly IEditionService _service;

    public EditionsController(IEditionService service)
    {
        _service = service;
    }

    // GET: api/Editions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Edition>>> GetEditions()
    {
        var editions = await _service.GetAllEditionsAsync();
        return Ok(editions);
    }

    // GET: api/Editions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Edition>> GetEdition(int id)
    {
        var edition = await _service.GetAsync(id);

        if (edition == null)
        {
            return NotFound();
        }

        return edition;
    }

    // PUT: api/Editions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutEdition(int id, EditionDTO editionDTO)
    {
        var edition = await _service.GetAsync(id);

        if (edition == null)
            return NotFound();

        edition.ReleaseDate = editionDTO.ReleaseDate;
        edition.Format = editionDTO.Format;
        edition.BookId = editionDTO.BookId;

        await _service.UpdateAsync(edition);
        return NoContent();
    }

    // POST: api/Editions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Edition>> PostEdition(EditionDTO editionDTO)
    {
        var edition = new Edition
        {
            ReleaseDate = editionDTO.ReleaseDate,
            Format = editionDTO.Format,
            BookId = editionDTO.BookId
        };


        await _service.AddAsync(edition);

        return CreatedAtAction(nameof(GetEdition), new { id = edition.Id }, edition);
    }

    // DELETE: api/Editions/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEdition(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
