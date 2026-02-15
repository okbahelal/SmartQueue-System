using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartQueue.Api.Data;
using SmartQueue.Api.Models;
using System.Linq;

namespace SmartQueue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly QueueDbContext _db;

    public TicketsController(QueueDbContext db)
    {
        _db = db;
    }

   [HttpPost]
public async Task<ActionResult<Ticket>> Create([FromQuery] string serviceName)
{
    string code = serviceName switch
    {
        "إيداع" => "D",
        "سحب" => "W",
        "فتح حساب" => "A",
        "استفسار" => "Q",
        _ => "G"
    };

    int lastSeq = await _db.Tickets
        .Where(t => t.ServiceCode == code)
        .OrderByDescending(t => t.SequenceNumber)
        .Select(t => t.SequenceNumber)
        .FirstOrDefaultAsync();

    int nextSeq = lastSeq + 1;

    var ticket = new Ticket
    {
        ServiceName = serviceName,
        ServiceCode = code,
        SequenceNumber = nextSeq,
        DisplayNumber = $"{code}-{nextSeq:000}"
    };

    _db.Tickets.Add(ticket);
    await _db.SaveChangesAsync();

    return Ok(ticket);
}


    [HttpGet]
    public async Task<ActionResult<List<Ticket>>> GetAll()
    {
        var list = await _db.Tickets.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        return Ok(list);
    }
}
