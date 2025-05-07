using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Models.Surveys;

namespace server.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SurveyTemplate> SurveyTemplates { get; set; }
        public DbSet<SurveyQuestionTemplate> QuestionTemplates { get; set; }
        public DbSet<AnswerOptionTemplate> AnswerOptionTemplates { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<SurveyResponseAnswer> SurveyResponseAnswers { get; set; }
        public DbSet<MessageType> MessageTypes {get; set;}
        public DbSet<SmsOptInAudit> SmsOptInAudits {get; set;}
        public DbSet<SmsStatus> SmsStatuses {get; set;}
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageTemplate> MessageTemplates {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Create a demo contact
            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Name = "Demo User", PhoneNumber = "+1234567890", OptInTime = new DateTime(2025, 1, 1) }
            );

            // Create a demo survey template
            modelBuilder.Entity<SurveyTemplate>().HasData(
                new SurveyTemplate { Id = 1, SurveyName = "TextDemo", CreatedAt = new DateTime(2025, 1, 1) }
            );


            modelBuilder.Entity<SurveyResponseAnswer>()
                .HasOne(sra => sra.SurveyResponse)
                .WithMany(sr => sr.Answers)
                .HasForeignKey(sra => sra.SurveyResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SurveyResponseAnswer>()
                .HasOne(sra => sra.SurveyQuestionTemplate)
                .WithMany()
                .HasForeignKey(sra => sra.SurveyQuestionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyResponseAnswer>()
                .HasOne(sra => sra.AnswerOptionTemplate)
                .WithMany()
                .HasForeignKey(sra => sra.AnswerOptionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyQuestionTemplate>()
                .HasOne(qt => qt.SurveyTemplate)
                .WithMany(st => st.Questions)
                .HasForeignKey(qt => qt.SurveyTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }
    }
}
