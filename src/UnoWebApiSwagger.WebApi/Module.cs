using Microsoft.Extensions.DependencyInjection;

namespace UnoWebApiSwagger.WebApi
{
    public static class Module
    {
        public static void Register(IServiceCollection builder)
        {
            builder.AddTransient<ITokenRepository, TokenRepository>();
        }
    }
}