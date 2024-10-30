using Exam.Models;

namespace Exam.Repositories
{
    public interface IExamTakedService
    {
        Task<ExamTaked> Add(ExamTaked examTaked);
        ExamTaked GetExam(string username , int examId);

        Task<List<ExamTaked>> GetExamListIfItisnotGradedYet();

        ExamTaked Update(ExamTaked examTaked);
    }
}
