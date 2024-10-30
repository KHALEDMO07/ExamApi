using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required , MaxLength(1000)]
        public string Body {  get; set; }
        public QuestionAnswer Answer { get; set; }
        public Exam exam { get; set; }
        public int examId { get; set; }

        public SubmitedAnswers SubmitedAnswer { get; set; }

        //public QuestionAnswer Answer { get; set; }

        //public int QuestionAnswerId { get; set; }
    }
}
