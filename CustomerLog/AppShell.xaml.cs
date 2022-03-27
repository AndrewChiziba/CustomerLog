using CustomerLog.ViewModels;
using CustomerLog.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CustomerLog
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Incoming), typeof(Incoming));
            Routing.RegisterRoute(nameof(Outgoing), typeof(Outgoing));
            Routing.RegisterRoute(nameof(TransactionsPage), typeof(TransactionsPage));
        }

    }
}
