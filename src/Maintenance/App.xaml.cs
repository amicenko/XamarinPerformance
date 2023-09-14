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
            var dataStore = DependencyService.Get<MockDataStore>();
            _repeater = Observable.Interval(TimeSpan.FromMinutes(2))
                .Do(async tick =>
                {
                    foreach (var cachedItem in await dataStore.GetItemsAsync(true))
                    {
                        for (var i = 0; i < cachedItem.Images.Length; ++i)
                        {
                            var newImage = await DummyClient.GetImageStream();
                            cachedItem.Images[i] = new Models.Image(newImage) { Name = "Downloaded " + i };
                            await dataStore.UpdateItemAsync(cachedItem);
                        }

                    }
                })
                .Subscribe();
        }
    }
}
