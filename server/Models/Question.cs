namespace server.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int QuestionNumber { get; set; }
        public string Text { get; set; } = "";

        public List<Answer> PossibleAnswers { get; set; } = new();
        public bool IsRequired { get; set; }
        public int OrderInSurvey { get; set; }
        public QuestionType Type { get; set; }
    }
    public enum QuestionType
    {
        YesNo,
        MultipleChoice,
        Rating,
        Text
    }
}
