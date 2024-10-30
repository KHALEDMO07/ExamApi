using Exam.Services;

namespace Exam.Middlewares
{
    public class JwtBlacklistedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenBlacklistService _tokenBlacklistService;
        public JwtBlacklistedMiddleware(RequestDelegate next , ITokenBlacklistService tokenBlacklistService)
        {
            _next = next;
            _tokenBlacklistService = tokenBlacklistService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if(!string.IsNullOrEmpty(token) )
            {
                if (await _tokenBlacklistService.IsTokenBlacklistedAsync(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("The User Is Logged Out.");
                    return;
                }
            }
             await _next(context);
        }
    }
}
