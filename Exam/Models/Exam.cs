using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Exam
    {
        public int Id { get; set; }


        // public string Title {  get; set; }
        [Required , MaxLength(50)]
        public string Subject { get; set; }
        [Required , MaxLength(50)]
        public string Type { get; set;  } 
        public int DurationInMinutes { get; set; }
        public List<Question>Questions { get; set; }

        public User user { get; set; }

        public int userId { get; set; }
    }
}
