using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace server.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly HttpClient _httpClient;

        public MessagingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(string phoneNumber, string messageContent)
        {
            // Here you would implement the logic to send the message.
            // This could involve calling an external SMS API.

            var messageData = new
            {
                to = phoneNumber,
                body = messageContent
            };

            var json = JsonSerializer.Serialize(messageData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Example API endpoint (replace with your actual SMS provider's endpoint)
            var response = await _httpClient.PostAsync("https://api.yoursmsprovider.com/send", content);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error (log it, throw an exception, etc.)
                throw new Exception("Failed to send message");
            }
        }
    }
}