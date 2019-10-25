using System.Threading.Tasks;
using System.Windows.Input;
using UnoMvvm;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class RatesViewModel : LoadViewModel
    {
        private readonly IRateWebClient _rateWebClient;
        private Rates _rates;

        public RatesViewModel(IRateWebClient rateWebClient)
        {
            _rateWebClient = rateWebClient;
            RefreshCommand = new DelegateCommand(async () => await Load());
        }

        public Rates Rates
        {
            get => _rates;
            set => SetProperty(ref _rates, value);
        }

        public ICommand RefreshCommand { get; }

        public override async Task Load()
        {
            Rates = await _rateWebClient.GetAsync();
        }
    }
}
