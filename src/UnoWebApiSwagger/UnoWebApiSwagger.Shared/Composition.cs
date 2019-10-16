using System.Collections.Generic;
using Windows.UI.Xaml;
using ButchersQA.Uwp;
using Grace.DependencyInjection;
using UnoMvvm;
using UnoWebApiSwagger.ClientContracts;
using UnoWebApiSwagger.ViewModels;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger
{
    public class Composition
    {
        private static DependencyInjectionContainer _container;

        public Composition()
        {
            _container = CreateContainer();
        }

        private static DependencyInjectionContainer CreateContainer()
        {
            var builder = new DependencyInjectionContainer();
            builder.Configure(c => c.Export<MainPageViewModel>());
            builder.Configure(c => c.Export<RateWebTestClient>().As<IRateWebClient>());
            builder.Configure(c => c.Export<EventAggregator>().As<IEventAggregator>());
            builder.Configure(c => c.Export<LogService>().As<ILogService>());
            builder.Configure(c => c.Export<UnoMvvm.Navigation.NavFrame>().As<INavService>());
            builder.Configure(c => c.Export<BaseUrlConfig>().As<IBaseUrlConfig>());
            builder.Configure(c => c.Export<TokenClientConfig>().As<ITokenClientConfig>());
            builder.Configure(c => c.Export<TokenRepository>().As<ITokenRepository>());
            builder.Configure(c => c.Export<UnoMvvm.Navigation.DispatcherUiService>().As<IDispatcherUiService>());

            return builder;
        }

        public static MainPageViewModel Root => _container.Locate<MainPageViewModel>();
    }
}