using CustomerLog.Models;
using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CustomerLog.Services
{
    class CustomerServices
    {
        static SQLiteAsyncConnection Database;

        async static Task Init()
        {
            if (Database != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MobileMoney.db");

            Database = new SQLiteAsyncConnection(databasePath);

            await Database.CreateTableAsync<Customer>();
            await Database.CreateTableAsync<Transaction>();
        }

        public static async Task AddCustomer(string phonenumber, string name)
        {
            await Init();

            var customer = new Customer
            {
                PhoneNumber = phonenumber,
                Name = name
            };

            await Database.InsertAsync(customer);
        }

        public static async Task RemoveCustomer(int id)
        {
            await Init();
            await Database.DeleteAsync<Customer>(id);
        }

        public static async Task<IEnumerable<Customer>> GetCustomers()
        {
            await Init();
            var customers = await Database.Table<Customer>().ToListAsync();
            return customers;
        }

        public static async Task<Customer> GetCustomer(int id)
        {
            await Init();
            var customer = await Database.Table<Customer>().FirstOrDefaultAsync(x => x.Id == id);
            return customer;
        }

        public static async Task AddCustomer(IEnumerable<Customer> customers)
        {
            await Init();
            await Database.InsertAllAsync(customers);
        }

        //Transaction area
        public static async Task AddTransaction(DateTime datetime, double amount, int customerid, bool isoutgoing)
        {
            await Init();
            var transaction = new Transaction
            {
                Date = datetime,
                Amount = amount,
                CustomerFK = customerid,
                IsOutgoing = isoutgoing
            };

            await Database.InsertAsync(transaction);
        }

        public static async Task AddTransaction(IEnumerable<Transaction> transactions)
        {
            await Init();
            var re = await Database.InsertAllAsync(transactions);
        }


        public static async Task<IEnumerable<Transaction>> GetTransactions()
        {
            await Init();
            var transactions = await Database.Table<Transaction>().ToListAsync();
            return transactions;
        }

        public static async Task<IEnumerable<TransactionDisplay>> GetTransactionsToDisplay()
        {
            await Init();
            var customers = await GetCustomers();
            var transactions = await Database.Table<Transaction>().ToListAsync();
            var listoftransactionstodisplay = transactions.Select(x => new TransactionDisplay
            {
                Date = x.Date,
                Amount = x.Amount,
                TransactionCustomer = customers.FirstOrDefault(y => y.Id == x.CustomerFK),
                IsOutgoing = x.IsOutgoing
            });
            return listoftransactionstodisplay;
        }

    }
}
