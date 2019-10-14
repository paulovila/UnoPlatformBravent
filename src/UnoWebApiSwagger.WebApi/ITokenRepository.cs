using System.Threading.Tasks;

namespace UnoWebApiSwagger.WebApi
{
    public interface ITokenRepository
    {
        Task<SessionDto> Authenticate(string username, string password);
    }
}