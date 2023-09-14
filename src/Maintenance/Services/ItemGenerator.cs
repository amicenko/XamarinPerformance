using Common;
using Maintenance.Models;
using System;
using Image = Maintenance.Models.Image;

namespace Maintenance.Services
{
    internal static class ItemGenerator
    {
        public static Item[] GetSmallCache()
        {
            return GetCacheWithCount(15);
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
                    Images = new Image[] { new Image(ImageGenerator.GenerateImage().Result), new Image(ImageGenerator.GenerateImage().Result) }
                };
            }

            return items;
        }
    }
}
