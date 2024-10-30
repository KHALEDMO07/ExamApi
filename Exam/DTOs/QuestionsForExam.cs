using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class QuestionsForExam
    {
        public int Id { get; set; }

        [Required, MaxLength(1000)]
        public string Body { get; set; }


    }
}
