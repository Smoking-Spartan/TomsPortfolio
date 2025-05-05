namespace server.Models{
    public class SmsOptInAudit
    {
        public int Id {get; set;}
        public required string PhoneNumber {get; set;}
        public int? ContactId {get; set;}
        public required DateTime OptinTime {get; set;}
        public required string IPAddress {get; set;}
        public string UserAgent {get; set;} = "";
        public bool WasSuccessful {get; set;} = true; 
    }
}