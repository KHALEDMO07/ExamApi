using System.ComponentModel.DataAnnotations;

namespace Exam.DTOs
{
    public class RefreshModelDto
    {
        [Required]
        public string accessToken { get; set; }
        [Required]  
        public string refreshToken { get; set; }
    }
}
