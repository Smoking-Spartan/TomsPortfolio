using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SurveyTemplate> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed demo survey questions
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    QuestionNumber = 1,
                    Text = "What is your thoughts on the demo so far? (1 being awful and 10 being perfect)",
                    IsRequired = true,
                    OrderInSurvey = 1,
                    Type = QuestionType.Rating
                },
                new Question
                {
                    Id = 2,
                    QuestionNumber = 2,
                    Text = "Would you recommend this demo to a friend or family member?",
                    IsRequired = true,
                    OrderInSurvey = 2,
                    Type = QuestionType.YesNo
                },
                new Question
                {
                    Id = 3,
                    QuestionNumber = 3,
                    Text = "What do you like so far about the demo?",
                    IsRequired = false,
                    OrderInSurvey = 3,
                    Type = QuestionType.MultipleChoice
                },
                new Question
                {
                    Id = 4,
                    QuestionNumber = 4,
                    Text = "Any thoughts or suggestions?",
                    IsRequired = false,
                    OrderInSurvey = 4,
                    Type = QuestionType.Text
                }
            );

            // Seed possible answers for multiple choice question
            modelBuilder.Entity<Answer>().HasData(
                new Answer { Id = 1, QuestionId = 3, Response = "The Text Messages" },
                new Answer { Id = 2, QuestionId = 3, Response = "The iMessage like preview" },
                new Answer { Id = 3, QuestionId = 3, Response = "The Slack Opt-in Page" },
                new Answer { Id = 4, QuestionId = 3, Response = "Ease of Use" },
                new Answer { Id = 5, QuestionId = 3, Response = "The Survey itself" },
                new Answer { Id = 6, QuestionId = 3, Response = "N/A" }
            );
        }
    }
}
