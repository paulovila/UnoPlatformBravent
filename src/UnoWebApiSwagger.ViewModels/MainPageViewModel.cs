using System.Threading.Tasks;
using UnoMvvm;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IRateWebClient _rateWebClient;
        private Rates _rates;
        public MainPageViewModel(IRateWebClient rateWebClient) => _rateWebClient = rateWebClient;

        public Rates Rates
        {
            get => _rates;
            set => SetProperty(ref _rates, value);
        }
        public async Task Load() => Rates = await _rateWebClient.GetAsync();
    }
}