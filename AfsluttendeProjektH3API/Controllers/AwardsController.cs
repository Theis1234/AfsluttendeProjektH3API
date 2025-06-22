using AfsluttendeProjektH3API.Application;
using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Application.Services;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfsluttendeProjektH3API;

[Route("api/[controller]")]
[ApiController]
public class AwardsController : ControllerBase
{
    private readonly IAwardService _service;

    public AwardsController(IAwardService service)
    {
        _service = service;
    }

    // GET: api/Awards
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Award>>> GetAwards()
    {
        var awards = await _service.GetAllAwardsAsync();
        return Ok(awards);
    }

    // GET: api/Awards/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Award>> GetAward(int id)
    {
        var award = await _service.GetAsync(id);

        if (award == null)
        {
            return NotFound();
        }

        return award;
    }

    // PUT: api/Awards/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutAward(int id, AwardDTO awardDTO)
    {
        var award = await _service.GetAsync(id);

        if (award == null)
            return NotFound();

        award.ArtistId = awardDTO.ArtistId;
        award.DateReceived = awardDTO.DateReceived;
        award.Description = awardDTO.Description;
        award.Name = awardDTO.Name;

        await _service.UpdateAsync(award);
        return NoContent();
    }

    // POST: api/Awards
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Award>> PostAward(AwardDTO awardDTO)
    {
        var award = new Award
        {
            ArtistId = awardDTO.ArtistId,
            DateReceived = awardDTO.DateReceived,
            Description = awardDTO.Description,
            Name = awardDTO.Name
        };


        await _service.AddAsync(award);

        return CreatedAtAction(nameof(GetAward), new { id = award.Id }, award);
    }

    // DELETE: api/Awards/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAward(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
