using Exam.DTOs;
using Exam.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Exam.Repositories
{
    public class AuthenticationService(Jwt JwtOptions) : IAuthenticationService
    {
        public async Task<JwtSecurityToken> CreateJwtToken(CreateJwtTokenParametersDto dto)
        {
            var authClaims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier , dto.Id.ToString()),
                new Claim(ClaimTypes.Name , dto.FullName), 
                new Claim(ClaimTypes.Role , dto.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key));

            var token = new JwtSecurityToken
            (
                issuer : JwtOptions.Issuer,
                audience: JwtOptions.Audience,  // Add Audience if applicable
                claims: authClaims,                 // Add a list of claims if you have any
                expires: DateTime.UtcNow.AddSeconds(60), // Set token expiration time
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

            );

            return token;
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<ClaimsPrincipal?> GetPrincipleFromExpiredToken(string token)
        {
           var key = Encoding.UTF8.GetBytes(JwtOptions.Key);//the key of our jwt token
            var validition = new TokenValidationParameters
            {
                ValidIssuer = JwtOptions.Issuer , 
                ValidAudience = JwtOptions.Audience ,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false // we must make it false because will compare with expired token
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validition,out _);
        }
    }
}
