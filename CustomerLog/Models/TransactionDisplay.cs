using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerLog.Models
{
    public class TransactionDisplay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public Customer TransactionCustomer { get; set; }

        public bool IsOutgoing { get; set; }
    }
}
