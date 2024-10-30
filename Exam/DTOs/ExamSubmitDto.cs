namespace Exam.DTOs
{
    public class ExamSubmitDto
    {
       // public int Id { get; set; }
        public int ExamId { get; set; }

        public string UserName { get; set; }

        public List<SubmitedAnswersDto> submitedAnswers { get; set; }
    }
}
