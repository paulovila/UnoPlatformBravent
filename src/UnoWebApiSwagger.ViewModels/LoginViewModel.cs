using System.Threading.Tasks;
using System.Windows.Input;
using UnoMvvm;
using UnoWebApiSwagger.WebApiClient;

namespace UnoWebApiSwagger.ViewModels
{
    public class LoginViewModel : BindableBase, IViewModel
    {
        private readonly ITokenClientConfig _tokenClientConfig;
        private readonly INavService _navService;
        private string _userName;
        private string _password;

        public LoginViewModel(ITokenClientConfig tokenClientConfig, INavService navService)
        {
            _tokenClientConfig = tokenClientConfig;
            _navService = navService;
            LoginCommand = new DelegateCommand(async () => await DoLogin())
                    .ObservesProperty(() => UserName)
                    .ObservesProperty(() => Password);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }
        public async Task DoLogin()
        {
            _tokenClientConfig.User = UserName;
            _tokenClientConfig.Password = Password;
            if (!string.IsNullOrEmpty(await _tokenClientConfig.GetToken()))
                _navService.Navigate<RatesViewModel>();
        }
    }
}
