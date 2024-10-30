using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class User
    {
       
        public int Id { get; set; }

        [Required , MaxLength(50)]  
        public string UserName { get; set; }
        [Required, MaxLength(255)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is Required"), MaxLength(255)]

        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [RegularExpression("^(Admin|Student|Teacher)$", ErrorMessage = "The type must be either admin, student, or teacher.")]
        public string Role { get; set; }

        public List<Exam>?Exams { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }

    }
}
