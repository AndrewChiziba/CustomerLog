using CustomerLog.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CustomerLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Incoming : ContentPage
    {
        public Incoming()
        {
            InitializeComponent();
            BindingContext = new TransactionViewModel();
        }

        private void ListView_ItemTapped(Object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}