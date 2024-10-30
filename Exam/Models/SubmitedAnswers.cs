using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class SubmitedAnswers
    {
        [Key]
        public int Id { get; set; }

        public int ExamId { get; set;  }
        [Required , MaxLength(250)]
        public string UserName { get; set; }
        [Required , MaxLength(250)]
        public string Answer { get; set; }

        public Question question { get; set; }

        public int QuestionId { get; set; }

    }

}
