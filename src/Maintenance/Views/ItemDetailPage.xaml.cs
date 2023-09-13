using Maintenance.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Maintenance.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}