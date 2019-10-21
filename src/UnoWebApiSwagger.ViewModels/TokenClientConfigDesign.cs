using System.Threading;
using System.Threading.Tasks;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class TokenClientConfigDesign : ITokenClientConfig
    {
        public string BaseUrl { get; }
        public string User { get; set; }
        public string Password { get; set; }
        public Task<string> GetToken(CancellationToken cancellationToken) => Task.FromResult("_");
    }
}