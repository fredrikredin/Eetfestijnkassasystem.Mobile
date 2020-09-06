using System.ComponentModel;
using Xamarin.Forms;
using Eetfestijnkassasystem.Mobile.ViewModels;

namespace Eetfestijnkassasystem.Mobile.Views
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