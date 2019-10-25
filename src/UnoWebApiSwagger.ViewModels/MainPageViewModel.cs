using System;
using UnoMvvm;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavService _navService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDispatcherUiService _dispatcherUiService;
        private string _error;
        public MainPageViewModel(INavService navService, IEventAggregator eventAggregator, IDispatcherUiService dispatcherUiService)
        {
            _navService = navService;
            _eventAggregator = eventAggregator;
            _dispatcherUiService = dispatcherUiService;
            _navService.NavigationFailed = e => _eventAggregator.GetEvent<ErrorEvent>().Publish(e);
            _eventAggregator.GetEvent<ErrorEvent>().Subscribe(LogError);
            _dispatcherUiService.Run(() => _navService.Navigate<LoginViewModel>());
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