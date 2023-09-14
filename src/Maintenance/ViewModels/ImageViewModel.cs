using Maintenance.Models;
using System;

namespace Maintenance.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        private bool isThumbnail;
        private Image imageModel;

        public bool IsThumbnail
        {
            get => isThumbnail;
            set => SetProperty(ref isThumbnail, value);
        }

        public int Width => isThumbnail ? Math.Min(150, Image.Width) : Math.Max(150, Image.Width);
        public int Height => isThumbnail ? Math.Min(150, Image.Height) : Math.Max(150, Image.Height);

        public Image Image
        {
            get => imageModel;
            set
            {
                for (int i = 0; i < 10; ++i)
                {
                    // Simulate dodgy code that was setting properties one by one and triggering updates for each.
                    SetProperty(ref imageModel, value);
                }
            }
        }

        public byte[] Data
        {
            get => imageModel?.Data;
            set => imageModel.Data = value;
        }
    }
}
