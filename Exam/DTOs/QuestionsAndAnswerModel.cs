using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class QuestionsAndAnswerModel
    {
        [Required , MaxLength(1000)]
        public string Question { get; set; }

        [Required , MaxLength(1000)]

        public string Answer { get; set;}

        
    }
}
