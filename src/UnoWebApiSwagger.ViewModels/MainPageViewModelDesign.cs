using System;
using UnoMvvm;

namespace UnoWebApiSwagger.ViewModels
{
    public class MainPageViewModelDesign : MainPageViewModel
    {
        public MainPageViewModelDesign() : base(new NavServiceDesign(), new EventAggregator(), new DispatcherUiService())
        {
            Error = "Object reference not set to an instance on an object";
        }
    }

    public class DispatcherUiService : IDispatcherUiService
    {
        public void Run(Action action)
        {
            
        }
    }

    public class NavServiceDesign : INavService
    {
        public void Navigate<T>() where T : IViewModel
        {
            
        }

        public void Navigate<T, TP>(TP parameters) where T : IViewModel
        {
        }

        public void Clear()
        {
        }

        public Action<Exception> NavigationFailed { get; set; }
    }
}