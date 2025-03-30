namespace server.Services
{
    public interface IMessagingService
    {
        Task SendMessageAsync(string phoneNumber, string messageContent);
    }
}