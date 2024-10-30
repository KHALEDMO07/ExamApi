namespace Exam.DTOs
{
    public class LoginResponseDto
    {
        public required string accessToken { get; set; }

        public required DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; }    
    }
}
