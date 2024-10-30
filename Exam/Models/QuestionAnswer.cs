using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        [Required , MaxLength(1000)]
        public string Answer { get; set; }

        public Question Question { get; set; }

        public int QuestionId { get; set; }
    }
}
