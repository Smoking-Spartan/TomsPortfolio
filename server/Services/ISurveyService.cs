using server.Models;

namespace server.Services
{
    public interface ISurveyService
    {
        Task SaveSurveyResponseAsync(SurveyResponse response);
        Task<List<SurveyResponse>> GetSurveyResponsesAsync(string surveyId);
    }
} 