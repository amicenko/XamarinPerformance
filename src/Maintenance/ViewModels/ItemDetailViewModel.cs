using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Maintenance.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private ImageViewModel[] images;

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

        public ImageViewModel[] Images
        {
            get => images;
        }

        public async void LoadItemId(Guid itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
