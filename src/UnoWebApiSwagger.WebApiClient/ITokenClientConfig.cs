using System.Threading;
using System.Threading.Tasks;

namespace UnoWebApiSwagger.WebApiClient
{
    public interface ITokenClientConfig : IBaseUrlConfig
    { 
        string User { get; set; }
        string Password { get; set; }
        Task<string> GetToken(CancellationToken cancellationToken = default);
    }
}