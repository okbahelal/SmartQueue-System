using SmartQueue.Api.Models;

namespace SmartQueue.Api.Services
{
    public interface ITicketService
    {
        object CreateTicket(string serviceName);

        Task<object> GetAllAsync(
            int page = 1,
            int pageSize = 10,
            string? serviceName = null,
            bool? isServed = null);

        object GetServiceStatus(string serviceName);
        bool ServeTicket(int id);
        Ticket? ServeNext(string serviceName);

    }
}



