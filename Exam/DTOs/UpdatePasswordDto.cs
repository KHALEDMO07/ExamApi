using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class UpdatePasswordDto
    {
        [Required, MaxLength(50)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string ConfirmedPassword { get; set; }
    }
}
