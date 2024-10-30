using Exam.Models;

namespace Exam.Repositories
{
    public interface IQuestionService
    {
        Question GetById(int id);
    }
}
