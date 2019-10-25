using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace UnoWebApiSwagger.WebApi.Controllers
{
    public class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ITokenRepository _tokenRepository;

        public TokenController(IOptions<JwtIssuerOptions> jwtOptions, ITokenRepository tokenRepository)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _tokenRepository = tokenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string username, string password)
        {
            SessionDto sessionDto = await _tokenRepository.Authenticate(username, password);
            if (sessionDto != null)
            {
                return new ObjectResult(GenerateToken(sessionDto));
            }

            return BadRequest();
        }

        private async Task<Token> GenerateToken(SessionDto sessionDto)
        {
            var claim1 = new ClaimsIdentity(new GenericIdentity(sessionDto.UserName, "Token"), new[] {
                          new Claim("staff", sessionDto.UserName)

            }).FindFirst("staff");
            Claim[] claims = {
                new Claim(JwtRegisteredClaimNames.Sub, sessionDto.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(ClaimTypes.NameIdentifier, sessionDto.UserId.ToString()),
                new Claim(ClaimTypes.Name, sessionDto.UserName),
                new Claim(ClaimTypes.Role, sessionDto.RoleCode),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds().ToString()),
                claim1
            };

            // Create the JWT security token and encode it.
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            return new Token
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };
        }



        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}