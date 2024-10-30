
namespace Exam.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly List<string>_blacklist = new List<string>();
        public Task AddTokenToBlacklistAsync(string token)
        {
            _blacklist.Add(token);
            return Task.CompletedTask;
        }

        public Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return Task.FromResult(_blacklist.Contains(token));
               
        }
    }
}
