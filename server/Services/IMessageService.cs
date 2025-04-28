using server.Models;

namespace server.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(Message message);
    }
} 