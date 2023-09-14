using System;

namespace Maintenance.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public Image[] Images { get; set; }
    }
}