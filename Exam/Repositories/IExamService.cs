namespace Exam.Repositories
{
    public interface IExamService
    {
        Task<Exam.Models.Exam> Add(Exam.Models.Exam exam);

        Exam.Models.Exam GetRandomExam(string subject);
    }
}
