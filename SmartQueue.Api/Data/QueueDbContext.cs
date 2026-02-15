using Microsoft.EntityFrameworkCore;
using SmartQueue.Api.Models;

namespace SmartQueue.Api.Data;

public class QueueDbContext : DbContext
{
    public QueueDbContext(DbContextOptions<QueueDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();
}
