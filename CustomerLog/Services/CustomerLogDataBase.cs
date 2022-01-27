using CustomerLog.Models;
using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

    }
}
