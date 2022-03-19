using CustomerLog.Models;
using CustomerLog.Services;
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
        public ObservableRangeCollection<TransactionDisplay> Transactions { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand RemoveCommand { get; }

        public TransactionViewModel()
        {
            Title = "Transactions";

            Transactions = new ObservableRangeCollection<TransactionDisplay>();

            RefreshCommand = new AsyncCommand(Refresh);

            //new Action(async () => await Refresh())();
        }

        public TransactionDisplay selectedTransaction;

        public TransactionDisplay SelectedTransaction
        {
            get => selectedTransaction;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.TransactionCustomer.Name, "OK");
                    value = null;
                }
                selectedTransaction = value;
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

        public async Task Refresh()
        {
            IsBusy = true;
            //await AddDummyCustomers();
            //await AddDummyTransactions;
            await Task.Delay(2000);
            IsBusy = false;
            Transactions.Clear();
            var listofcustomers = await CustomerServices.GetTransactionsToDisplay();
            Transactions.AddRange(listofcustomers);
            IsBusy = false;
        }
    }

}
