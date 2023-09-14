using Maintenance.Models;

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
    }
}
