using AfsluttendeProjektH3API.Application;
using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfsluttendeProjektH3API;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : ControllerBase
{
    private readonly IEducationService _service;

    public EducationsController(IEducationService service)
    {
        _service = service;
    }

    // GET: api/Educations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Education>>> GetEducations()
    {
        var editions = await _service.GetAllEducationsAsync();
        return Ok(editions);
    }

    // GET: api/Educations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Education>> GetEducation(int id)
    {
        var education = await _service.GetAsync(id);

        if (education == null)
        {
            return NotFound();
        }

        return education;
    }

    // PUT: api/Educations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutEducation(int id, UpdateEducationDTO educationDTO)
    {
        var education = await _service.GetAsync(id);

        if (education == null)
            return NotFound();

        education.Institution = educationDTO.Institution;
        education.GraduationYear = educationDTO.GraduationYear;
        education.Degree = educationDTO.Degree;

        await _service.UpdateAsync(education);
        return NoContent();
    }

    // POST: api/Educations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Education>> PostEducation(CreateEducationDTO educationDTO)
    {
        var education = new Education
        {
            Institution = educationDTO.Institution,
            GraduationYear = educationDTO.GraduationYear,
            Degree = educationDTO.Degree
        };


        await _service.AddAsync(education);

        return CreatedAtAction(nameof(GetEducation), new { id = education.Id }, education);
    }

    // DELETE: api/Educations/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEducation(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
