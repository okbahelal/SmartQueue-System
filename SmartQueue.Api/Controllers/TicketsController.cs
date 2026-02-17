using Microsoft.AspNetCore.Mvc;
using SmartQueue.Api.Services;
using SmartQueue.Api.Models;

namespace SmartQueue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;

    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    // POST /api/Tickets?serviceName=...
    [HttpPost]
    public IActionResult CreateTicket([FromQuery] string serviceName)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
            return BadRequest("serviceName is required.");

        var result = _service.CreateTicket(serviceName);
        return Ok(result);
    }

    // GET /api/Tickets?page=1&pageSize=10&serviceName=...&isServed=false
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? serviceName = null,
        [FromQuery] bool? isServed = null)
    {
        var result = await _service.GetAllAsync(page, pageSize, serviceName, isServed);
        return Ok(result);
    }

    // GET /api/Tickets/status?serviceName=...
    [HttpGet("status")]
    public IActionResult GetServiceStatus([FromQuery] string serviceName)
    {
        if (string.IsNullOrWhiteSpace(serviceName))
            return BadRequest("serviceName is required.");

        var result = _service.GetServiceStatus(serviceName);
        return Ok(result);
    }

    // POST /api/Tickets/{id}/serve
    [HttpPost("{id}/serve")]
    public IActionResult ServeTicket(int id)
    {
        var ok = _service.ServeTicket(id);
        if (!ok) return NotFound();

        return Ok(new { message = "Ticket served successfully" });
    }
    // POST /api/Tickets/serve-next?serviceName=...
[HttpPost("serve-next")]
public IActionResult ServeNext([FromQuery] string serviceName)
{
    if (string.IsNullOrWhiteSpace(serviceName))
        return BadRequest("serviceName is required.");

    var result = _service.ServeNext(serviceName);
    if (result == null) return NotFound(new { message = "No waiting tickets for this service." });

    return Ok(new
    {
        message = "Next ticket served successfully",
        servedTicketId = result.Id,
        serviceName = result.ServiceName
    });
}

}




