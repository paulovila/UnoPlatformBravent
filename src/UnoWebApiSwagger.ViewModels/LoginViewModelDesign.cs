using UnoMvvm.Navigation;

namespace UnoWebApiSwagger.ViewModels
{
    public class LoginViewModelDesign : LoginViewModel
    {
        public LoginViewModelDesign() : base(new TokenClientConfigDesign(), new NavServiceDesign())
        {
            UserName = "user1";
            Password = "pass1";
            LastError = "last error text 1";
        }
    }
}