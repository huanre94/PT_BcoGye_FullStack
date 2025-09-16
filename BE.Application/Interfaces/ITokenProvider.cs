namespace BE.Application.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateToken(Guid userId, string userName);
        string GenerateToken(Guid userId, string userName, string role);
    }
}
