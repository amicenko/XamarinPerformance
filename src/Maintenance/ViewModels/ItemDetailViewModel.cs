using DynamicData;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Maintenance.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private ObservableCollection<ImageViewModel> images = new ObservableCollection<ImageViewModel>();

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get => itemId;

            set
            {
                itemId = value;
                LoadItemId(Guid.Parse(value));
            }
        }

        public ObservableCollection<ImageViewModel> Images
        {
            get => images;
            // set => SetProperty(ref images, value);
        }

        public async void LoadItemId(Guid itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Text = item.Text;
                Description = item.Description;
                images.Clear();
                images.AddRange(item.Images.Select(i => new ImageViewModel { Image = i, Title = "lol", IsThumbnail = true }));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
