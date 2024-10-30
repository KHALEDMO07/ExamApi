using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data
{
    public class AppDbContext  : DbContext
    {
        public DbSet<User> Users { get; set; }  
        public DbSet<Exam.Models.Exam>Exams { get; set; }
        public DbSet<Question>Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

        public DbSet<ExamTaked> ExamsTaked { get; set; }

        public DbSet<SubmitedAnswers>SubmitedAnswers { get; set; }

        public AppDbContext(DbContextOptions options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamTaked>().HasKey(e => new { e.examId, e.UserName });
        }
    }
}
