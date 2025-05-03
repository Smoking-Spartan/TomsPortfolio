using Microsoft.AspNetCore.Mvc;
using server.Services;
using Vonage;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceLifetimeTestController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        private readonly ISurveyService _surveyService;
        private readonly VonageClient _vonageClient;
        private readonly ILogger<ServiceLifetimeTestController> _logger;

        public ServiceLifetimeTestController(
            IMessagingService messagingService,
            ISurveyService surveyService,
            VonageClient vonageClient,
            ILogger<ServiceLifetimeTestController> logger)
        {
            _messagingService = messagingService;
            _surveyService = surveyService;
            _vonageClient = vonageClient;
            _logger = logger;
        }

        [HttpGet("test")]
        public IActionResult TestLifetimes()
        {
            // Get the hash codes of the service instances
            var messagingServiceHash = _messagingService.GetHashCode();
            var surveyServiceHash = _surveyService.GetHashCode();
            var vonageClientHash = _vonageClient.GetHashCode();

            _logger.LogInformation($"MessagingService Hash: {messagingServiceHash}");
            _logger.LogInformation($"SurveyService Hash: {surveyServiceHash}");
            _logger.LogInformation($"VonageClient Hash: {vonageClientHash}");

            return Ok(new
            {
                MessagingServiceHash = messagingServiceHash,
                SurveyServiceHash = surveyServiceHash,
                VonageClientHash = vonageClientHash,
                Message = "Check the logs to see the hash codes. Make multiple requests to see how they change."
            });
        }
    }
} 