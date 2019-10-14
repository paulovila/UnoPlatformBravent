namespace UnoWebApiSwagger.WebApiClient
{
    public partial class BaseWebClient
    {
        public BaseWebClient(IBaseUrlConfig urlConfig) => BaseUrl = urlConfig.BaseUrl;
        public string BaseUrl { get; set; }
    }
}