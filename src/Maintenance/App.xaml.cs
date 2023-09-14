using Akavache;
using Maintenance.Client;
using Maintenance.Services;
using System;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Maintenance
{
    public partial class App : Application
    {
        private IDisposable _repeater;
        private DummyClient _client;

        public App()
        {
            InitializeComponent();
            Registrations.Start("com.gsmcwarl.Maintenance");
            BlobCache.EnsureInitialized();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            PollServer();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            PollServer();
        }

        private void PollServer()
        {
            _repeater = Observable.Interval(TimeSpan.FromMinutes(1))
                .Do(tick =>
                {

                })
                .Subscribe();
        }
    }
}
