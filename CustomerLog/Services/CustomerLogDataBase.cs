using CustomerLog.Models;
using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLog.Services
{
    class CustomerLogDataBase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<CustomerLogDataBase> Instance = new AsyncLazy<CustomerLogDataBase>(async () =>
        {
            var instance = new CustomerLogDataBase();
            CreateTableResult Customers = await Database.CreateTableAsync<Customer>();
            CreateTableResult Transactions = await Database.CreateTableAsync<Transaction>();
            return instance;
        });

        public CustomerLogDataBase()
        {
            if(Database != null)
            {
                return;
            }
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public static async Task AddCustomer(string phonenumber, string name, double amount)
        {
            var customer = new Customer
            {
                PhoneNumber = phonenumber,
                Name = name,
                Amount = amount
            };

            await Database.InsertAsync(customer);
        }

        public static async Task RemoveCustomer(int id)
        {
            await Database.DeleteAsync<Customer>(id);
        }

        public static async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await Database.Table<Customer>().ToListAsync();
            return customers;
        }
    }
}
