using System;
using UnoMvvm;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IDispatcherUiService _dispatcherUiService;
        private string _error;
        public MainPageViewModel(INavService navService, IEventAggregator eventAggregator, IDispatcherUiService dispatcherUiService)
        {
            _dispatcherUiService = dispatcherUiService;
            navService.NavigationFailed = e => eventAggregator.GetEvent<ErrorEvent>().Publish(e);
            eventAggregator.GetEvent<ErrorEvent>().Subscribe(LogError);
            _dispatcherUiService.Run(navService.Navigate<LoginViewModel>);
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }
        private void LogError(Exception ex)
        {
            if (ex == null) return;
            _dispatcherUiService.Run(() => Error = ex.Message);
        }
    }
}