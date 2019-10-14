using System.Threading.Tasks;
using System.Windows.Input;
using UnoMvvm;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IRateWebClient _rateWebClient;
        private Rates _rates;

        public MainPageViewModel(IRateWebClient rateWebClient)
        {
            _rateWebClient = rateWebClient;
            RefreshCommand = new DelegateCommand(OnRefresh);
        }

        public Rates Rates
        {
            get => _rates;
            set => SetProperty(ref _rates, value);
        }

        public ICommand RefreshCommand { get; }

        private async void OnRefresh()
        {
            Rates = await _rateWebClient.GetAsync();
        }

        public async Task Load()
        {
            Rates = await _rateWebClient.GetAsync();
        }
    }
}