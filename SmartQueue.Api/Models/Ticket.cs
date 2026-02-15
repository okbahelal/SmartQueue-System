using System.ComponentModel.DataAnnotations;

namespace SmartQueue.Api.Models;

public class Ticket
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;

    public string ServiceCode { get; set; } = "G";
    public int SequenceNumber { get; set; }
    public string DisplayNumber { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsServed { get; set; } = false;
}

