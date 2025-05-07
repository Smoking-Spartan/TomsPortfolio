using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class SurveyQuestionTemplate
    {
        public int Id { get; set; }
        public int QuestionNumber { get; set; }
        public string Text { get; set; } = "";
        [ForeignKey("SurveyTemplate")]
        public int SurveyTemplateId { get; set; }
        public SurveyTemplate? SurveyTemplate { get; set; }
        public bool IsRequired { get; set; }
        public int OrderInSurvey { get; set; }
        public QuestionType Type { get; set; }
        public List<AnswerOptionTemplate>? AnswerOptions { get; set; }
    }
    public enum QuestionType
    {
        YesNo,
        MultipleChoice,
        Rating,
        Text
    }
}
