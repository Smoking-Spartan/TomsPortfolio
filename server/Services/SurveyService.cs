using Microsoft.EntityFrameworkCore;
using server.Models;
using server.data;

namespace server.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly AppDbContext _context;

        public SurveyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveSurveyResponseAsync(SurveyResponse response)
        {
            _context.SurveyResponses.Add(response);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SurveyResponse>> GetSurveyResponsesAsync(int surveyId)
        {
            return await _context.SurveyResponses
                .Include(sr => sr.Answers)
                .Where(sr => sr.SurveyTemplateId == surveyId)
                .ToListAsync();
        }
    }
} 