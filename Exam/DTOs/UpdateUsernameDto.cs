using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class UpdateUsernameDto
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }
    }
}
