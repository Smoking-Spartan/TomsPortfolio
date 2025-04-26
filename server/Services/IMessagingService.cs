using Vonage;
using Vonage.Messaging;
using Vonage.Request;

namespace server.Services
{
    public interface IMessagingService
    {
        Task SendMessageAsync(string phoneNumber, string messageContent);
    }
}