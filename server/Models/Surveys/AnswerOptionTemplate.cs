using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models{
    public class AnswerOptionTemplate
    {
        public int Id { get; set; }
        [ForeignKey("SurveyQuestionTemplate")]
        public int SurveyQuestionTemplateId { get; set; }
        public SurveyQuestionTemplate? SurveyQuestionTemplate { get; set; }

        public string Text { get; set; } = string.Empty; // What the user sees
        public string? Value { get; set; }               // Optional backend value
        public int DisplayOrder { get; set; }
    }
}
