namespace Exam.DTOs
{
    public class ExamTakedDto
    {
        public int ExamId { get; set; }

        public List<QuestionsForExam>ExamQuestions { get; set; }
    }
}
