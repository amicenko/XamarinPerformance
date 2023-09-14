using System.IO;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Image = Maintenance.Models.Image;

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
                for (int i = 0; i < 5; ++i)
                {
                    // Simulate dodgy code that was setting properties one by one and triggering updates for each.
                    SetProperty(ref imageModel, null);
                    SetProperty(ref imageModel, value);
                }
            }
        }

        public ImageSource ImageSource =>
            ImageSource.FromStream(() =>
            {
                try
                {
                    var test = SixLabors.ImageSharp.Image.Load(Image?.Data);
                }
                catch
                {
                }

                return new MemoryStream(Image?.Data);
            });

        [IgnoreDataMember]
        public string Length => Image?.Data?.Length.ToString();
    }
}
