   namespace server.Models{
    public class MessageType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum MessageTypeEnum
    {
        Alert,
        OptIn,
        OptOut,
        SurveyLink,
        SurveyResults
    }
} 
