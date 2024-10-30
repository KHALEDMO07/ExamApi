
using Exam.Data;
using Exam.Models;

namespace Exam.Repositories
{
    public class ExamTakedService : IExamTakedService
    {
        private readonly AppDbContext _context;
        public ExamTakedService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ExamTaked> Add(ExamTaked examTaked)
        {
            await _context.ExamsTaked.AddAsync(examTaked);

            return examTaked;
        }

        public ExamTaked GetExam(string username , int examId)
        {
            var exam =  _context.ExamsTaked.FirstOrDefault(e => e.UserName == username && e.examId == examId);

            return exam;
        }

        public Task<List<ExamTaked>> GetExamListIfItisnotGradedYet()
        {
            return Task.FromResult(_context.ExamsTaked.Where(e => e.grade == 0).ToList());
        }

        public ExamTaked Update(ExamTaked examTaked)
        {
            _context.Update(examTaked);
            return examTaked;
        }
    }
}
