using BE.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BE.Infrastructure.Security
{
    public class JwtHandler : ITokenProvider
    {
        //esto debe ir el configuration 
        public const string SecretKey = "Pru3b4T3cn1c4Backend2025SuperSecretKeyForJWT";
        public string GenerateToken(Guid userId, string userName)
        {
            var tokenResponse = string.Empty;
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, userName));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            tokenResponse = tokenHandler.WriteToken(securityToken);

            return tokenResponse;
        }

        public string GenerateToken(Guid userId, string userName, string role)
        {
            var tokenResponse = string.Empty;
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, userName));
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            tokenResponse = tokenHandler.WriteToken(securityToken);

            return tokenResponse;
        }
    }
}
