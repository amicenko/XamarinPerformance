using Akavache;
using Maintenance.Client;
using Maintenance.Services;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
            _repeater = Observable.Interval(TimeSpan.FromMinutes(1))
                .Do(async tick =>
                {
                    var items = await dataStore.GetItemsAsync(true);
                    await Task.WhenAll(items.Select(async item =>
                    {
                        var updatedImages = item.Images.Select(image => DummyClient.GetImageStream());
                        await Task.WhenAll(updatedImages);

                        for (int i = 0; i < updatedImages.Count(); i++)
                        {
                            var downloadStream = updatedImages.ElementAt(i).Result;
                            using (var ms = new MemoryStream())
                            {
                                await downloadStream.CopyToAsync(ms);
                                item.Images[i].Data = ms.ToArray();
                            }
                        }
                    }));
                })
                .Subscribe();
        }
    }
}
