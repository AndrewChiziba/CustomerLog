using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerLog.Models
{
    class Transaction
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        [ForeignKey("Customer")]
        public int CustomerFK { get; set; }

        public bool isOutgoing { get; set; }
    }
}
