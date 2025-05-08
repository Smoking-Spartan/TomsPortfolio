namespace server.Models.DTO {
    public class SurveyQuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; } // e.g., "Text", "YesNo", "Rating"
        public bool IsRequired { get; set; }
        public int OrderInSurvey { get; set; }
        public List<string>? Options { get; set; } // Only for Multiple Choice
    }   
}