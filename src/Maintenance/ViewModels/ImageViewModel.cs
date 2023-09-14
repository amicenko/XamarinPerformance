using System.Runtime.Serialization;
using System.Threading.Tasks;
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
                    SetProperty(ref imageModel, value);
                }
            }
        }

        [IgnoreDataMember]
        public ImageSource ImageSource =>
            ImageSource.FromStream(ctx => Task.FromResult(Image?.Stream));

        [IgnoreDataMember]
        public string Length => Image?.Data?.Length.ToString();
    }
}
