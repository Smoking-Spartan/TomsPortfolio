using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class SurveyTemplate
    {
        public int Id { get; set; }
        public required string SurveyName {get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Question> Questions {get; set;} = new();
        public List<Answer> Answers { get; set; } = new();
    }
}
