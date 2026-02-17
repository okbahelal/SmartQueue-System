using System;

namespace SmartQueue.Api.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsServed { get; set; } = false;
    }
}

