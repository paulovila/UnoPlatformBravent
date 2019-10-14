using UnoWebApiSwagger.WebApiClient;

namespace ButchersQA.Uwp
{
    internal class BaseUrlConfig : IBaseUrlConfig
    {
        public string BaseUrl => "http://localhost:55255";
        //public string BaseUrl => "https://localhost:44344";
    }
}