using System;
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
        private string _lastError;

        public LoginViewModel(ITokenClientConfig tokenClientConfig, INavService navService)
        {
            _tokenClientConfig = tokenClientConfig;
            _navService = navService;
            LoginCommand = new DelegateCommand(async () => await DoLogin(),()=>!string.IsNullOrWhiteSpace( UserName) && !string.IsNullOrWhiteSpace(Password))
                    .ObservesProperty(() => UserName)
                    .ObservesProperty(() => Password);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string LastError
        {
            get => _lastError;
            set => SetProperty(ref _lastError , value);
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
                LastError = null;

                if (!string.IsNullOrEmpty(await _tokenClientConfig.GetToken()))
                    _navService.Navigate<RatesViewModel>();
                else
                    LastError = "Login or password is invalid";

        }
    }
}
