using Exam.DTOs;
using Exam.Models;

namespace Exam.Repositories
{
    public interface IUserService
    {
        Task<User> Add(User user);
        bool IsValidData(string userName , string Password = null);

        User GetByIdentity(UserLogin user);

        User GetById(int id);   

        User Update(User user);
        User DeleteUser(User user);

        Task<List<User>> GetAll();

    }
}
