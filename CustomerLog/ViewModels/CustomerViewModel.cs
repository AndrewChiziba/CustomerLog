using CustomerLog.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerLog.ViewModels
{
    public class CustomerViewModel: ViewModelBase
    {
        public ObservableRangeCollection<Customer> Customers { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand RemoveCommand { get; }

        public CustomerViewModel()
        {
            Title = "Customer";
            Customers = new ObservableRangeCollection<Customer>();
        }
    }
}
