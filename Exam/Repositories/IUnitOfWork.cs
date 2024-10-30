namespace Exam.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        IUserService _userService { get; }
        IExamService _examService { get; }
        IExamTakedService _examTakedService { get; }
        IQuestionService _questionService { get; }  
        ISubmittedAnswersService _submittedAnswersService { get; }
        int Complete();
        
    }
}
