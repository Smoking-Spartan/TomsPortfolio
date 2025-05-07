using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class SurveyTemplate
    {
        public int Id { get; set; }
        public required string SurveyName {get; set; }
        public DateTime CreatedAt { get; set; }
        public List<SurveyQuestionTemplate> Questions {get; set;} = new();
        public List<AnswerOptionTemplate> Answers { get; set; } = new();
    }
}
