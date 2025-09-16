using BE.Application.Interfaces;
using BE.Domain.Dto;
using BE.Domain.Interfaces;

namespace BE.Application.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenProvider _tokenProvider;
        readonly IUserRepository _userRepository;

        public AuthService(ITokenProvider tokenProvider, IUserRepository userRepository)
        {
            _tokenProvider = tokenProvider;
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> ValidateLogin(LoginDto login)
        {
            var user = await _userRepository.GetUserByUsername(login.Email, login.Password);
            if (user == null) return null;
            
            var token = _tokenProvider.GenerateToken(user.Id, user.UserName, user.Role.ToString());
            return new LoginResponse { AccessToken = token };
        }
    }
}
