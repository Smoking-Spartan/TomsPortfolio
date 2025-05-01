using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class SurveyTemplate
    {
        public int Id { get; set; }
        
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime CreatedAt { get; set; }

        public int ContactId { get; set; }
        public Contact? Contact { get; set; }

        public List<Answer> Answers { get; set; } = new();
    }
}
