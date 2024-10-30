namespace Exam.Services
{
    public interface ITokenBlacklistService
    {
        Task<bool> IsTokenBlacklistedAsync(string token);
        Task AddTokenToBlacklistAsync(string token);
    }
}
