using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class MessageTemplate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("MessageType")] // Assumes you have a MessageType model
        public int MessageTypeId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string TemplateText { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        // Navigation property
        public MessageType? MessageType { get; set; }
    }
} 