using Microsoft.EntityFrameworkCore;
using SmartQueue.Api.Data;
using SmartQueue.Api.Models;

namespace SmartQueue.Api.Services
{
    public class TicketService : ITicketService
    {
        private readonly QueueDbContext _db;
        private const int MinutesPerPerson = 5;

        public TicketService(QueueDbContext db)
        {
            _db = db;
        }

        public object CreateTicket(string serviceName)
        {
            var ticket = new Ticket
            {
                ServiceName = serviceName,
                CreatedAt = DateTime.UtcNow,
                IsServed = false
            };

            _db.Tickets.Add(ticket);
            _db.SaveChanges();

            var waiting = _db.Tickets.Count(t => t.ServiceName == serviceName && !t.IsServed);
            var estimatedMinutes = Math.Max(0, waiting * MinutesPerPerson);

            return new
            {
                id = ticket.Id,
                serviceName = ticket.ServiceName,
                createdAt = ticket.CreatedAt,
                waiting,
                estimatedMinutes,
                minutesPerPerson = MinutesPerPerson
            };
        }

        public async Task<object> GetAllAsync(
            int page = 1,
            int pageSize = 10,
            string? serviceName = null,
            bool? isServed = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var query = _db.Tickets.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(serviceName))
                query = query.Where(t => t.ServiceName == serviceName);

            if (isServed.HasValue)
                query = query.Where(t => t.IsServed == isServed.Value);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new
            {
                page,
                pageSize,
                totalCount,
                totalPages,
                data
            };
        }

        public object GetServiceStatus(string serviceName)
        {
            var waiting = _db.Tickets.Count(t => t.ServiceName == serviceName && !t.IsServed);
            var estimatedMinutes = Math.Max(0, waiting * MinutesPerPerson);

            return new
            {
                serviceName,
                waiting,
                estimatedMinutes,
                minutesPerPerson = MinutesPerPerson
            };
        }

        public bool ServeTicket(int id)
        {
            var ticket = _db.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null) return false;

            ticket.IsServed = true;
            _db.SaveChanges();
            return true;
        }
        public Ticket? ServeNext(string serviceName)
{
    var ticket = _db.Tickets
        .Where(t => t.ServiceName == serviceName && !t.IsServed)
        .OrderBy(t => t.Id) // الأقدم أولاً
        .FirstOrDefault();

    if (ticket == null) return null;

    ticket.IsServed = true;
    _db.SaveChanges();
    return ticket;
}

    }
}

