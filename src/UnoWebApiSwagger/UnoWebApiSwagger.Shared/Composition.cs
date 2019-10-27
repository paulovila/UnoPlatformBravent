using Grace.DependencyInjection;
using UnoMvvm;

namespace UnoWebApiSwagger
{
    public class Composition
    {
        public static DependencyInjectionContainer Container;
        
        public static void CreateContainer()
        {
            var builder = new DependencyInjectionContainer();
            builder.Configure(c => c.Export<ViewModels.MainPageViewModel>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<WebApiClient.RateWebClient>().As<WebApiClient.IRateWebClient>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<EventAggregator>().As<IEventAggregator>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<UnoMvvm.Navigation.NavFrame>().As<INavService>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<Shared.BaseUrlConfig>().As<WebApiClient.IBaseUrlConfig>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<ViewModels.TokenClientConfig>().As<WebApiClient.ITokenClientConfig>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<WebApiClient.TokenRepository>().As<WebApiClient.ITokenRepository>().Lifestyle.Singleton());
            builder.Configure(c => c.Export<UnoMvvm.Navigation.DispatcherUiService>().As<IDispatcherUiService>().Lifestyle.Singleton());
            RegisterTypeForNavigation<Shared.Rates, ViewModels.RatesViewModel>(builder);
            RegisterTypeForNavigation<Shared.Login, ViewModels.LoginViewModel>(builder);
            Container = builder;
        }
        public static void RegisterTypeForNavigation<TV, TVM>(DependencyInjectionContainer builder)
        {
            ViewModelLocationProvider.Register<TV, TVM>();
            builder.Configure(c => c.Export<TVM>());
        }
    }
}