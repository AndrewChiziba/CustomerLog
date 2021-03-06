using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerLog.Models
{
    [NotMapped]
    public class TransactionDisplay
    {
        public Customer TCustomer { get; set; }
        public IEnumerable<Transaction> TTransactions { get; set; }
    }
}
