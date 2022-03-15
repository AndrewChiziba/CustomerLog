using CustomerLog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}