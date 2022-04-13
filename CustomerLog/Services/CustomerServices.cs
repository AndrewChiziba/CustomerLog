using CustomerLog.Models;
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
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            //Assign to customer variable, and transaction variable
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction
            
        }
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
            var transactions = await Database.Table<Transaction>().Where(x => x.CustomerFK == customerid).ToListAsync();
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


        //Processes the messages 
        public static async Task ProcessMessage(string Message)
        {
            //Determine whether incoming or outgoing. [function] incoming-keyword "recieved" outgoing-keywords "withdrawn, Payment...Successful, Sent.
            bool IsOutgoing = CustomerServices.IsMessageOutgoing(Message);
            //Assign to customer variable, and transaction variable
            Customer customer = new Customer
            {

            };
            //Check if customer exists, using Phone number. If customer exists then add transaction, if not first create customer and then Add transaction

        }

        // Function to assatain whether a transaction is incoming or outgoing
        public static bool IsMessageOutgoing(string message)
        {
            string s1 = "Txn. ID : LP220326.0836.G55985. Payment to AMMOBILE Account NA of ZMW 10.00 is successful. Your Airtel Money balance is ZMW 219.98",
            s2 = "Use NEW short code *115# for Airtel Money. Txn. ID : PP220324.0947.J19373. You have sent ZMW 26.00 to 978108812,Andrew Chiziba . Your available balance is ZMW 691.78.",
            s3 = "Use NEW short code *115# for Airtel Money. You have withdrawn ZMW 700.00 from 974343542 Boster Hambubu. Your balance is ZMW 748.29. Txn. ID: CO220323.1601.L96515.",
            s4 = "Dear Customer, you have received ZMW 600.00 from 978122020 CLARENCE BEENZU. Dial *115# to check your new balance. Txn ID: PP220323.1557.L16505.";

            message = s1;

            if (message.Contains("recieved"))
            {
                return false; // Message is not outgoing
            }
            else if (message.Contains("withdrawn"))
            {
                return true; // Message is outgoing
            }
            else if (message.Contains("Payment") && message.Contains("successful"))
            {
                return true; // Message is outgoing
            }
            else if (message.Contains("Sent"))
            {
                return true; // Message is outgoing
            }
            return false;
        } 

        public static string GetNumber(string Message)
        {
            return Message;
        }

        public static string GetName(string Message)
        {
            
            return Message;
        }

        public static string GetAmount(string Message)
        {
            return Message;
        }

    }
}
