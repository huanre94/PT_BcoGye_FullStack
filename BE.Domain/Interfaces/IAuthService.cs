using BE.Domain.Dto;

namespace BE.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> ValidateLogin(LoginDto login);
    }
}
