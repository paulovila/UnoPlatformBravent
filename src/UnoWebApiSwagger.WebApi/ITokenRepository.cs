using System.Threading.Tasks;

namespace UnoWebApiSwagger.WebApi
{
    public interface ITokenRepository
    {
        Task<SessionDto> Authenticate(string username, string password);
    }

    public class TokenRepository : ITokenRepository
    {
        public Task<SessionDto> Authenticate(string username, string password) => Task.FromResult(
            new SessionDto
            {
                UserName = username,
                RoleCode = "Role1",
                UserId = 33,
            });
    }
}