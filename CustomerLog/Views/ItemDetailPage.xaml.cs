using CustomerLog.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CustomerLog.Views
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