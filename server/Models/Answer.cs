namespace server.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Response { get; set; } = "";
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        public int SurveyId { get; set; }
        public SurveyTemplate? Survey { get; set; }
        public int ContactId { get; set; }
    }
}
