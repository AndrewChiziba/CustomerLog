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
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MobileMoney1.db");

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

        public static async Task ProcessMessage(string Message)
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }

        public static async Task AddTransaction(IEnumerable<Transaction> transactions)
        {
            await Init();
            var re = await Database.InsertAllAsync(transactions);
        }

        public static async Task AddTransactions(List<string> Messages)
        {
             
        }


        public static async Task<IEnumerable<Transaction>> GetTransactions()
        {
            await Init();
            var transactions = await Database.Table<Transaction>().ToListAsync();
            return transactions;
        }

        public static async Task<IEnumerable<CustomerDisplay>> GetCustomersToDisplay()
        {
            await Init();
            var customers = await GetCustomers();

            var transactions = await Database.Table<Transaction>().ToListAsync();

            var listofcustomerstodisplay = customers.Select(x => new CustomerDisplay
            {
                TCustomer = x,
                TTransaction = transactions.First(y => y.CustomerFK == x.Id)
            });

 
            return listofcustomerstodisplay;
        }

        public static async Task<TransactionDisplay> GetTransactionDisplay(int customerid)
        {
            await Init();
            var customer = await Database.Table<Customer>().FirstOrDefaultAsync(x => x.Id == customerid);
            var transactions = await Database.Table<Transaction>().Where(x => x.CustomerFK == customerid).ToArrayAsync();
            var transactionswithimages = transactions.Select(x => new Transaction
            {
                Id = x.Id,
                Date = x.Date,
                Amount = x.Amount,
                CustomerFK = x.CustomerFK,
                IsOutgoing = x.IsOutgoing,
                ImagePath = (x.IsOutgoing) ? "outgoingtransaction.png" : "incomingtransaction.png"
            });
            var transactiondisplay = new TransactionDisplay
            {
                TCustomer = customer,
                TTransactions = transactionswithimages
            };
            return transactiondisplay;
        }

    }
}
