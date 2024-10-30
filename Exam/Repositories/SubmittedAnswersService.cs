using Exam.Data;
using Exam.DTOs;
using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Repositories
{
    public class SubmittedAnswersService : ISubmittedAnswersService
    {
        private readonly AppDbContext _context;

        public SubmittedAnswersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SubmitedAnswers> Add(SubmitedAnswers submitedAnswers)
        {
            await _context.SubmitedAnswers.AddAsync(submitedAnswers);
            return submitedAnswers;
        }

        public List<SubmitedAnswers> GetByUsernameAndExamId(string username, int ExamId)
        {
            return _context.SubmitedAnswers.Where(s => s.UserName == username && s.ExamId == ExamId).ToList();
        }
    }
}
