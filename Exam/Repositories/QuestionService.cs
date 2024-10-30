using Exam.Data;
using Exam.Models;

namespace Exam.Repositories
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;
        public QuestionService(AppDbContext context)
        {
            _context = context;
        }
        public Question GetById(int id)
        {
            var Question = _context.Questions.Find(id);

            return Question;
        }
    }
}
