using UnoMvvm.Navigation;

namespace UnoWebApiSwagger.ViewModels
{
    public class LoginViewModelDesign : LoginViewModel
    {
        public LoginViewModelDesign() : base(new TokenClientConfigDesign(), new NavServiceDesign())
        {
        }
    }
}