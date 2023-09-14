using Common;
using Maintenance.Models;
using System;
using System.Linq;
using Image = Maintenance.Models.Image;

namespace Maintenance.Services
{
    internal static class ItemGenerator
    {
        public static Item[] GetSmallCache()
        {
            return GetCacheWithCount(15);
        }

        public static Item[] GetMediumCache()
        {
            return GetCacheWithCount(45);
        }

        public static Item[] GetLargeCache()
        {
            return GetCacheWithCount(100);
        }

        public static Item[] GetCacheWithCount(int count)
        {
            var items = new Item[count];
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = new Item
                {
                    Id = Guid.NewGuid(),
                    Text = i.ToString(),
                    Description = "I'm a small item at index " + i,
                    Images = new Image[count / 5].Select(x => new Image(ImageGenerator.GenerateImage().Result)).ToArray()
                };
            }

            return items;
        }
    }
}
