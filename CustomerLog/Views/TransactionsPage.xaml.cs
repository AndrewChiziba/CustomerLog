using CustomerLog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomerLog.Views
{
    [QueryProperty(nameof(CustomerId), nameof(CustomerId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {

        public string CustomerId { get; set; }
        public TransactionsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(CustomerId, out var result);
            var context = await CustomerServices.GetTransactionDisplay(result);
            BindingContext = context;
        }

        
    }
}

