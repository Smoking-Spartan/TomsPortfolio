namespace server.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int QuestionNumber { get; set; }
        public string Text { get; set; } = "";

        public List<Answer> Answers { get; set; } = new();
    }
}
