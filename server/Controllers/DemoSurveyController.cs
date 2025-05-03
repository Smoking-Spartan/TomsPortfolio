using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;
using Microsoft.EntityFrameworkCore;
using server.data;
using Vonage.Common;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoSurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IMessagingService _messageService;
        private readonly AppDbContext _context;

        public DemoSurveyController(ISurveyService surveyService, IMessagingService messageService, AppDbContext context)
        {
            _surveyService = surveyService;
            _messageService = messageService;
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitDemoSurvey([FromBody] DemoSurveySubmission submission)
        {
            try
            {
                // Create survey response
                var surveyResponse = new SurveyResponse
                {
                    SurveyId = 1, // Fixed ID for demo survey
                    ContactId = submission.ContactId,
                    StartedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow,
                    Answers = new List<Answer>
                    {
                        new Answer
                        {
                            QuestionId = 1,
                            Response = submission.Rating.ToString(),
                            ContactId = submission.ContactId,
                            SubmittedAt = DateTime.UtcNow
                        },
                        new Answer
                        {
                            QuestionId = 2,
                            Response = submission.Recommendation ?? "",
                            ContactId = submission.ContactId,
                            SubmittedAt = DateTime.UtcNow
                        },
                        new Answer
                        {
                            QuestionId = 3,
                            Response = string.Join(",", submission.Likes),
                            ContactId = submission.ContactId,
                            SubmittedAt = DateTime.UtcNow
                        },
                        new Answer
                        {
                            QuestionId = 4,
                            Response = submission.Comments.Suggestions,
                            ContactId = submission.ContactId,
                            SubmittedAt = DateTime.UtcNow
                        }
                    }
                };

                // Save the response
                await _surveyService.SaveSurveyResponseAsync(surveyResponse);

                // Generate and send follow-up message
                var followUpMessage = GenerateFollowUpMessage(submission);
                if (submission.ContactId != 0)
                {
                    var contact = submission.ContactId;
                    await _messageService.SendMessageAsync(new server.Models.Message
                    {
                        ContactId = contact,
                        Content = followUpMessage,
                        SentAt = DateTime.UtcNow
                    });
                }

                return Ok(new { message = "Survey submitted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to submit survey", details = ex.Message });
            }
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetSurveyQuestions()
        {
            try
            {
                var questions = await _context.Questions
                    .Include(q => q.PossibleAnswers)
                    .OrderBy(q => q.OrderInSurvey)
                    .ToListAsync();

                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to fetch survey questions", details = ex.Message });
            }
        }

        private string GenerateFollowUpMessage(DemoSurveySubmission submission)
        {
            var message = $"Thank you for your feedback! You rated our demo a {submission.Rating}/10";
            
            if (submission.Recommendation == "Yes")
            {
                message += " and would recommend it to others.";
            }
            else
            {
                message += " but wouldn't recommend it yet.";
            }

            if (submission.Likes.Any())
            {
                message += $"\n\nYou particularly liked: {string.Join(", ", submission.Likes)}.";
            }

            if (!string.IsNullOrEmpty(submission.Comments.Suggestions))
            {
                message += "\n\nWe appreciate your additional feedback and will take it into consideration!";
            }

            return message;
        }
    }

    public class DemoSurveySubmission
    {
        public int ContactId { get; set; }
        public int Rating { get; set; }
        public string? Recommendation { get; set; }
        public List<string> Likes { get; set; } = new();
        public SurveyComments Comments { get; set; } = new();
    }

    public class SurveyComments
    {
        public string Rating { get; set; } = "";
        public string Recommendation { get; set; } = "";
        public string Likes { get; set; } = "";
        public string Suggestions { get; set; } = "";
    }
} 