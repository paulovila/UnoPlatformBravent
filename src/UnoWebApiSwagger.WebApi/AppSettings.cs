namespace UnoWebApiSwagger.WebApi
{
    public class AppSettings
    {
        public bool EfLogSensitiveData { get; set; }
        public string EfConnectionString { get; set; }
        public string JwtTokenKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
    }
}