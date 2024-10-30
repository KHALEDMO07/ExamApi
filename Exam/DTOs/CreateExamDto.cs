using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class CreateExamDto
    {
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }
        [Required, MaxLength(50)]
        public string Type { get; set; }

        public int DurationInMinutes { get; set; }

        public List<QuestionsAndAnswerModel> Q_AModel { get; set; }
    }
}
