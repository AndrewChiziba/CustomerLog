using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomerLog.Models
{
    [NotMapped]
    public class CustomerDisplay
    {
        public Transaction TTransaction { get; set; }
        public Customer TCustomer { get; set; }

    }
}
