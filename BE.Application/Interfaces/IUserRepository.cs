using BE.Domain.Entities;

namespace BE.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username, string password);

    }
}
