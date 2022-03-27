using CustomerLog.Models;
using CustomerLog.Services;
using CustomerLog.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomerLog.ViewModels
{
    class TransactionViewModel: ViewModelBase
    {
        public ObservableRangeCollection<CustomerDisplay> Transactions { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand RemoveCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public TransactionViewModel()
        {
            Title = "Transactions";

            Transactions = new ObservableRangeCollection<CustomerDisplay>();

            RefreshCommand = new AsyncCommand(Refresh);

            SelectedCommand = new AsyncCommand<object>(Selected);

            new Action(async () => await Refresh())();
        }

        public CustomerDisplay selectedItem;

        public CustomerDisplay SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        async Task AddDummyTransactions()
        {
            var transactions = new ObservableRangeCollection<Transaction>
            {
                new Transaction { Date = DateTime.Parse("05/05/2021"), Amount = 50, CustomerFK = 1, IsOutgoing = false },
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 70, CustomerFK = 2, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 90 , CustomerFK = 3, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 20 , CustomerFK = 1, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 23 , CustomerFK = 2, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 87 , CustomerFK = 1, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 64 , CustomerFK = 2, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 75 , CustomerFK = 3, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 56 , CustomerFK = 1, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 12 , CustomerFK = 2, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 20 , CustomerFK = 3, IsOutgoing = false},
                new Transaction { Date = DateTime.Parse("05/05/2005"), Amount = 45 , CustomerFK = 1, IsOutgoing = false}
            };

            await CustomerServices.AddTransaction(transactions);
        }

        async Task AddDummyCustomers()
        {
            var customers = new ObservableRangeCollection<Customer>
            {
                new Customer { Name = "Natasha", PhoneNumber = "0770028215" },
                new Customer { Name = "Andrew", PhoneNumber = "0770028215" },
                new Customer { Name = "Kondwani", PhoneNumber = "0770028215" },
            };

            await CustomerServices.AddCustomer(customers);
        }

        public async Task Add(List<string> Messages)
        {

        }

        public async Task Selected(object args)
        {
            var customer = args as CustomerDisplay;
            if (customer == null)
                return;
            var route = $"{nameof(TransactionsPage)}?CustomerId={customer.TCustomer.Id}";
            var route1 = $"{nameof(Outgoing)}";
            await Shell.Current.GoToAsync(route);
        }

        public async Task Refresh()
        {
            IsBusy = true;
            //await AddDummyCustomers();
            //await AddDummyTransactions();
            await Task.Delay(2000);
            IsBusy = false;
            Transactions.Clear();
            var listofcustomers = await CustomerServices.GetCustomersToDisplay();
            Transactions.AddRange(listofcustomers);
            IsBusy = false;
        }
    }

}
