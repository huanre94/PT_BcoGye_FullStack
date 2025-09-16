using BE.Application.Interfaces;
using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BE.Infrastructure.Database
{
    public class UserRepository : IUserRepository
    {
        readonly BEContext _beContext;

        public UserRepository(BEContext beContext)
        {
            _beContext = beContext;
        }

        public async Task<User> GetUserByUsername(string username, string password)
        {
            var passwordHash = HashPassword(password);
            var user = await _beContext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password.SequenceEqual(passwordHash) && u.EsActivo);
            return user;
        }

        private byte[] HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
