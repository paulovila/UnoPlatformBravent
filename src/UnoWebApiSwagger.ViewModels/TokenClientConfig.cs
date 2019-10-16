using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnoWebApiSwagger.ClientContracts;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class TokenClientConfig : ITokenClientConfig
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IBaseUrlConfig _baseUrlConfig;

        public TokenClientConfig(ITokenRepository tokenRepository, IBaseUrlConfig baseUrlConfig)
        {
            _tokenRepository = tokenRepository;
            _baseUrlConfig = baseUrlConfig;
        }

        public string User { get; set; }
        public string Password { get; set; }
        public string BaseUrl => _baseUrlConfig.BaseUrl;

        private DateTime? _lastAccess;
        private string _lastToken;
        private TimeSpan _lastExpire;

        public async Task<string> GetToken(CancellationToken cancellationToken)
        {
            if (!_lastAccess.HasValue || DateTime.UtcNow - _lastAccess > _lastExpire)
            {
                FileResponse tokenFile = await _tokenRepository.CreateAsync(User, Password, cancellationToken);
                string tt = (new StreamReader(tokenFile.Stream)).ReadToEnd();
                Token result = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResult>(tt).result;
                _lastExpire = TimeSpan.FromSeconds(result.expires_in - 300);
                _lastToken = "Bearer " + result.access_token;
                _lastAccess = DateTime.UtcNow;
            }
            return _lastToken;
        }
    }
}