using Maintenance.Models;
using Maintenance.Services;
using Maintenance.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maintenance.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command FillSmallCommand { get; }
        public Command FillMediumCommand { get; }
        public Command FillLargeCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            FillSmallCommand = new Command(OnFillSmall);
            FillMediumCommand = new Command(OnFillMedium);
            FillLargeCommand = new Command(OnFillLarge);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnFillSmall(object obj)
        {
            foreach (var item in ItemGenerator.GetSmallCache())
            {
                await DataStore.AddItemAsync(item);
            }
        }

        private async void OnFillMedium(object obj)
        {
            foreach (var item in ItemGenerator.GetMediumCache())
            {
                await DataStore.AddItemAsync(item);
            }
        }

        private async void OnFillLarge(object obj)
        {
            foreach (var item in ItemGenerator.GetLargeCache())
            {
                await DataStore.AddItemAsync(item);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}