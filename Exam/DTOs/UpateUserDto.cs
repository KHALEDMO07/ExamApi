using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class UpateUserDto
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is Required"), MaxLength(255)]

        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [RegularExpression("^(Admin|Student|Teacher)$", ErrorMessage = "The type must be either admin, student, or teacher.")]
        public string Role { get; set; }
    }
}
