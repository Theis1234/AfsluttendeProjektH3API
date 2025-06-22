using AfsluttendeProjektH3API.Application;
using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfsluttendeProjektH3API;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _service;

    public GenresController(IGenreService service)
    {
        _service = service;
    }

    // GET: api/Genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        var genres = await _service.GetAllGenresAsync();
        return Ok(genres);
    }

    // GET: api/Genres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await _service.GetAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return genre;
    }

    // PUT: api/Genres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutGenre(int id, UpdateGenreDTO genreDTO)
    {
        var genre = await _service.GetAsync(id);

        if (genre == null)
            return NotFound();

        genre.Name = genreDTO.Name;

        await _service.UpdateAsync(genre);
        return NoContent();
    }

    // POST: api/Genres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Genre>> PostGenre(CreateGenreDTO genreDTO)
    {
        var genre = new Genre
        {
            Name = genreDTO.Name
        };


        await _service.AddAsync(genre);

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
