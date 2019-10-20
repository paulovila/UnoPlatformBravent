using UnoMvvm;
using UnoMvvm.Navigation;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModelDesign : MainPageViewModel
    {
        public MainPageViewModelDesign() : base(new NavServiceDesign(), new EventAggregator(), new DispatcherUiService())
        {
            Error = "Object reference not set to an instance on an object";
        }
    }
}