using Vonage;
using Vonage.Common;
using Vonage.Messaging;
using Vonage.Request;

namespace server.Services
{
    public interface IMessagingService
    {
        Task SendMessageAsync(Models.Message message);
    }
}