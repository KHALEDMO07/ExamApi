using Exam.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exam.Repositories
{
    public interface IAuthenticationService
    {
        Task<JwtSecurityToken> CreateJwtToken(CreateJwtTokenParametersDto dto);
        Task<string> GenerateRefreshToken();

        Task<ClaimsPrincipal?> GetPrincipleFromExpiredToken(string token); 
    }
}
