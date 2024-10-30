using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class TakeExamDto
    {
       
        public string UserName { get; set; }
        
        public string Subject { get; set; }
    }
}
