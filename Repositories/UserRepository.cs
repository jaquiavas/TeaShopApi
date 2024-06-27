using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TeaStoreApi.Data;
using TeaStoreApi.Interfaces;
using TeaStoreApi.Models;

namespace TeaStoreApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApiDbContext _context;
        public UserRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<User> LoginUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null && user.Password != HashPassword(password))
            {
                return null;
            }
            return user;
        }

        public async Task<bool> RegisterUser(User user)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userExists != null)
            {
                return false;
            }

            user.Password = HashPassword(user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256 .ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
