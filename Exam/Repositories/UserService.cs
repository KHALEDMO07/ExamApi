using Exam.Data;
using Exam.DTOs;
using Exam.Models;

namespace Exam.Repositories
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<User> Add(User user)
        {
              await _context.Users.AddAsync(user);


            return user;
        }

        public User DeleteUser(User user)
        {
            _context.Users.Remove(user);

            return user;
        }

        public  Task<List<User>> GetAll()
        {
            return Task.FromResult( _context.Users.ToList());
        }

        public User GetById(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }

        public User GetByIdentity(UserLogin user)
        {
            return  _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
        }

        public bool IsValidData(string userName , string Password = null)
        {
            if (Password == null)
            {
                return  _context.Users.Any(u => u.UserName == userName);
            }
            else return  _context.Users.Any(u => u.UserName == userName && u.Password == Password);
        }

        public User Update(User user)
        {
            _context.Update(user);
            return user;
        }
    }
}
