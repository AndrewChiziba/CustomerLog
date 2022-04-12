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

            
            RefreshCommand = new AsyncCommand(Refresh);

            new Action(async () => await Refresh())();
        }

        Customer selectedCustomer;

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if(value!= null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    value = null;
                }
                selectedCustomer = value;
                OnPropertyChanged();
            }
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
            await Task.Delay(2000);
            IsBusy = false;
            Customers.Clear();
            var listofcustomers = await CustomerServices.GetCustomers();
            Customers.AddRange(listofcustomers);
            IsBusy = false;
        }

    }


}
