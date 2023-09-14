using Akavache;
using Maintenance.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maintenance.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        //private static readonly string CachePath = Path.Combine(
        //    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        //    "GsmCwarl",
        //    "Maintenance",
        //    "cache.bin");

        //private static readonly string UwpPath = Path.Combine(
        //    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        //    "GsmCwarl",
        //    "Maintenance",
        //    "cache.bin");

        private readonly IBlobCache _cache = Device.RuntimePlatform == Device.UWP ? BlobCache.InMemory : BlobCache.Secure;

        public MockDataStore()
        {
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            await _cache.InsertObject(item.Id.ToString(), item);
            return true;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            // Don't do this!
            // var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();

            // Do this
            await _cache.Invalidate(item.Id.ToString());
            await _cache.InsertObject(item.Id.ToString(), item);

            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            await _cache.Invalidate(id.ToString());
            return true;
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = await _cache.GetObject<Item>(id.ToString());
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await _cache.GetAllObjects<Item>();
            return items;
        }
    }
}