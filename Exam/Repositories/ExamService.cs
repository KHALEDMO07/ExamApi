
using Exam.Data;
using Microsoft.EntityFrameworkCore;

namespace Exam.Repositories
{
    public class ExamService : IExamService
    {
        private readonly AppDbContext _context;
        public ExamService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Exam.Models.Exam> Add(Exam.Models.Exam exam)
        {
           await  _context.Exams.AddAsync(exam);

            return exam; 
        }

        public Models.Exam GetRandomExam(string subject)
        {
            var count = _context.Exams.Include(e=>e.Questions).Where(e => e.Subject == subject).ToList().Count();

            int randomIndex = new Random().Next(count);


            var randomExam = _context.Exams.Include(e=>e.Questions).Where(e => e.Subject == subject).ToList().Skip(randomIndex).FirstOrDefault();

            return randomExam;
        
                
        }
    }
}
