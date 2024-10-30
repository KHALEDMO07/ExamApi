using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class ExamTaked
    {
        [Key, Column(Order = 0)]
        public int examId { get; set; }
        [Key, Column(Order = 1)]
        public string UserName { get; set; }

        public int grade {  get; set; }
    }
}
