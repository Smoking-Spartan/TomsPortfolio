using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class QuestionType
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description {get; set;} ="";
        public string? InputControl {get; set;} = "";
        public string AllowOptions {get; set;} = "";
        public bool IsFreeText {get; set;} = false;
        public int? SortOrder {get; set;} 
    }
    public enum QuestionTypeEnum
    {
        YesNo,
        MultipleChoice,
        Rating,
        Text
    }
}
