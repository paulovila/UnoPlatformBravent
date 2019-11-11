using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UnoMvvm;
using UnoWebApiSwagger.ViewModels;

namespace UnoWebApiSwagger
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
           ViewModelLocationProvider.ViewModelFactory = t => Composition.Container.Locate(t);


            TaskScheduler.UnobservedTaskException += (s, e) => EventAggregator.GetEvent<ErrorEvent>().Publish(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => EventAggregator.GetEvent<ErrorEvent>().Publish(e.ExceptionObject as Exception);
            //AppDomain.CurrentDomain.FirstChanceException += (s, e) => EventAggregator.GetEvent<ErrorEvent>().Publish(e.Exception);
            this.UnhandledException += (s, e) =>
            {
                e.Handled = true;
                EventAggregator.GetEvent<ErrorEvent>().Publish(e.Exception);
            };
#if __WASM__
            var httpMessageHandler = Type.GetType("System.Net.Http.HttpClient, System.Net.Http")
                .GetField("GetHttpMessageHandler", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            httpMessageHandler.SetValue(null, (Func<System.Net.Http.HttpMessageHandler>)(() => new Uno.UI.Wasm.WasmHttpHandler()));
#endif
        }



        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                Composition.CreateContainer();
                EventAggregator = Composition.Container.Locate<IEventAggregator>();
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed +=(s1,e1)=> EventAggregator.GetEvent<ErrorEvent>().Publish(e1.Exception);

            Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.DataContext = Composition.Container.Locate<MainPageViewModel>();
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }
        public IEventAggregator EventAggregator;
    }
}
