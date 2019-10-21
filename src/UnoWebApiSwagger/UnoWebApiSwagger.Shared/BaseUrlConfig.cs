using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.Shared
{
    public class BaseUrlConfig : IBaseUrlConfig
    {
        public string BaseUrl => "http://localhost:20046/";
    }
}