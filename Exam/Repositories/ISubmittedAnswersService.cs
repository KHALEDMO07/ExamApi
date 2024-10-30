using Exam.DTOs;
using Exam.Models;

namespace Exam.Repositories
{
    public interface ISubmittedAnswersService
    {
        Task<SubmitedAnswers> Add(SubmitedAnswers submitedAnswers);
       List<SubmitedAnswers> GetByUsernameAndExamId(string username , int ExamId);
    }
}
