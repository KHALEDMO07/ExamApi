using Exam.DTOs;
using Exam.Models;
using Exam.Repositories;
using Exam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(Jwt JwtOptions , IUnitOfWork _unitOfWork 
        ,ITokenBlacklistService _tokenBlacklistService , IAuthenticationService _authenticationService) : ControllerBase
    {
     
       
        [HttpPost("RegisterUser")]
        public IActionResult Register(User user)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var isValidUserName =  _unitOfWork._userService.IsValidData(user.UserName,null); 

            if(isValidUserName) { return BadRequest("Your UserName is not Unique"); }
            
            _unitOfWork._userService.Add(user);
            _unitOfWork.Complete(); 

            return Ok(user); 
        }
        [HttpGet("Login")]

        public async Task<IActionResult> Login(UserLogin user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var User = _unitOfWork._userService.GetByIdentity(user);


            if (User == null)
            {
                return Unauthorized("UserName Or Password Are Wrong");
            }

            var claims = new CreateJwtTokenParametersDto
            {
                Id = User.Id,
                FullName = User.UserName,
                Role = User.Role
            }; 

            var AccessToken = await _authenticationService.CreateJwtToken(claims);
            var refreshToken = await _authenticationService.GenerateRefreshToken();

            User.RefreshToken = refreshToken;
            User.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _unitOfWork._userService.Update(User);
            _unitOfWork.Complete(); 

            var response = new LoginResponseDto
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(AccessToken),
                AccessTokenExpiration = AccessToken.ValidTo,
                RefreshToken = refreshToken

            };
            return Ok(response);
        }
        [HttpGet("Refresh")]
        public async Task<IActionResult> Refresh(RefreshModelDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var principal = await _authenticationService.GetPrincipleFromExpiredToken(dto.accessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();

            var userId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value); 

            var user = _unitOfWork._userService.GetById(userId);

            if (user is null || dto.refreshToken != user.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return Unauthorized(); 
            }

            var parameters = new CreateJwtTokenParametersDto
            {
                Id = userId,
                FullName = user.FullName, 
                Role = user.Role
            };
            var token = await _authenticationService.CreateJwtToken(parameters);

            var response = new LoginResponseDto
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token), 
                AccessTokenExpiration = token.ValidTo, 
                RefreshToken = dto.refreshToken
            };
            return Ok(response);
        }
        [HttpDelete("Revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId is null)return Unauthorized();

            var user = _unitOfWork._userService.GetById(Convert.ToInt32(userId)); 

            if(user is null) return Unauthorized();

            user.RefreshToken = null;

            _unitOfWork._userService.Update(user);
            _unitOfWork.Complete(); 

            return Ok();
        }
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> UserLogout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token Is Required");
            }
            await _tokenBlacklistService.AddTokenToBlacklistAsync(token);
            return Ok("Logged Out Successfully");
        }

       

        
        //[HttpPut("UpdateUser")]
        //  public IActionResult UpdateUser(User user) { }
        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(UserLogin user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var User = _unitOfWork._userService.GetByIdentity(user); 
            

            if(User == null)
            {
                return Unauthorized("Wrong UserName or Password");
            }

            _unitOfWork._userService.DeleteUser(User);
            _unitOfWork.Complete();
            return Ok(User);
        }
        [HttpGet("ReadUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _unitOfWork._userService.GetAll(); 

            if(users == null)
            {
                return NotFound("There is no users");
            }
            return Ok(users);
        }
        [HttpPut("UpdateUser")]
        [Authorize]

        public async Task<IActionResult> UpdateUser(UpateUserDto dto)
        {
            if(!ModelState.IsValid)return BadRequest(ModelState);
            var user = _unitOfWork._userService.
                GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if(user == null) { return BadRequest(); }

            user.FullName = dto.FullName;
            user.Email = dto.Email; 
            user.Role = dto.Role;

            _unitOfWork._userService.Update(user);

            _unitOfWork.Complete();


            return Ok(user);
        }
        [HttpPut("UpdateUsername")]
        [Authorize] 
        public async Task<IActionResult>UpdateUsername(UpdateUsernameDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool isExisted = _unitOfWork._userService.IsValidData(dto.UserName);    

            if(isExisted) { return BadRequest("There Is Someone uses This Username"); }

            var user = _unitOfWork._userService.
               GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier))); 

            if(user == null) { return BadRequest(); }

            user.UserName = dto.UserName;
            _unitOfWork._userService.Update(user); 
            _unitOfWork.Complete();

            return Ok(user);
        }

        [HttpPut("UpdatePassword")]
        [Authorize] 
        public async Task<IActionResult>UpdatePassword(UpdatePasswordDto dto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = _unitOfWork._userService.
             GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (user == null) { return BadRequest(); } 

            if(dto.Password != dto.ConfirmedPassword) { return BadRequest("Confirm Password Correctly!"); }

            user.Password = dto.ConfirmedPassword;

            return Ok(user);
        }
    }
}
