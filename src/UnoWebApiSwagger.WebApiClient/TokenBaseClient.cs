using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UnoWebApiSwagger.WebApiClient
{
    public class TokenBaseClient : BaseWebClient
    {
        private readonly ITokenClientConfig _tokenClientConfig;
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(15);

        public TokenBaseClient(ITokenClientConfig tokenClientConfig) : base(tokenClientConfig)
        {
            _tokenClientConfig = tokenClientConfig;
        }
        internal Task<HttpClient> CreateHttpClientAsync(CancellationToken cancellationToken) => Task.FromResult(new HttpClient { Timeout = _timeout });

        internal async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            string token = await _tokenClientConfig.GetToken(cancellationToken);
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Add("Authorization", token);
            return request;
        }
    }
}