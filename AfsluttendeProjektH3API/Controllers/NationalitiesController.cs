using AfsluttendeProjektH3API.Application;
using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AfsluttendeProjektH3API;

[Route("api/[controller]")]
[ApiController]
public class NationalitiesController : ControllerBase
{
    private readonly NationalityService _service;

    public NationalitiesController(NationalityService service)
    {
        _service = service;
    }

    // GET: api/Nationalitys
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nationality>>> GetNationalities()
    {
        var nationalitys = await _service.GetAllNationalitiesAsync();
        return Ok(nationalitys);
    }

    // GET: api/Nationalitys/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Nationality>> GetNationality(int id)
    {
        var nationality = await _service.GetAsync(id);

        if (nationality == null)
        {
            return NotFound();
        }

        return nationality;
    }

    // PUT: api/Nationalitys/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNationality(int id, UpdateNationalityDTO nationalityDTO)
    {
        var nationality = await _service.GetAsync(id);

        if (nationality == null)
            return NotFound();

        nationality.CountryName = nationalityDTO.CountryName;
        nationality.CountryCode = nationalityDTO.CountryCode;

        await _service.UpdateAsync(nationality);
        return NoContent();
    }

    // POST: api/Nationalitys
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Nationality>> PostNationality(CreateNationalityDTO nationalityDTO)
    {
        var nationality = new Nationality
        {
            CountryName = nationalityDTO.CountryName,
            CountryCode = nationalityDTO.CountryCode
        };


        await _service.AddAsync(nationality);

        return CreatedAtAction(nameof(GetNationality), new { id = nationality.Id }, nationality);
    }

    // DELETE: api/Nationalitys/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNationality(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
