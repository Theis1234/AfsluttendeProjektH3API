using AfsluttendeProjektH3API.Application;
using AfsluttendeProjektH3API.Application.DTOs;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AfsluttendeProjektH3API;

[Route("api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly PublisherService _service;

    public PublishersController(PublisherService service)
    {
        _service = service;
    }

    // GET: api/Publishers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
    {
        var publishers = await _service.GetAllPublishersAsync();
        return Ok(publishers);
    }

    // GET: api/Publishers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Publisher>> GetPublisher(int id)
    {
        var publisher = await _service.GetAsync(id);

        if (publisher == null)
        {
            return NotFound();
        }

        return publisher;
    }

    // PUT: api/Publishers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPublisher(int id, UpdatePublisherDTO publisherDTO)
    {
        var publisher = await _service.GetAsync(id);

        if (publisher == null)
            return NotFound();

        publisher.ContactEmail = publisherDTO.ContactEmail;
        publisher.Name = publisherDTO.Name;

        await _service.UpdateAsync(publisher);
        return NoContent();
    }

    // POST: api/Publishers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Publisher>> PostPublisher(CreatePublisherDTO publisherDTO)
    {
        var publisher = new Publisher
        {
            ContactEmail = publisherDTO.ContactEmail,
            Name = publisherDTO.Name
        };


        await _service.AddAsync(publisher);

        return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisher);
    }

    // DELETE: api/Publishers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
