namespace server.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime OptInTime { get; set; } = DateTime.UtcNow;
        public DateTime OptOutTime { get; set; } = DateTime.MinValue;
        public DateTime LastActiveTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public List<SurveyTemplate> Surveys { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
    }
}
