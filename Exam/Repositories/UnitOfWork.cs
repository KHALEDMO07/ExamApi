using Exam.Data;

namespace Exam.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserService _userService { get; private set; }

        public IExamService _examService { get; private set; }

        public IExamTakedService _examTakedService { get; private set; }

        public IQuestionService _questionService { get; private set; }

        public ISubmittedAnswersService _submittedAnswersService { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context; 
            _userService = new UserService(context);
            _examService = new ExamService(context);
            _examTakedService = new ExamTakedService(context);
            _questionService = new QuestionService(context);
            _submittedAnswersService = new SubmittedAnswersService(context);

        }

       // IUserService IUnitOfWork._userService => throw new NotImplementedException();

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
