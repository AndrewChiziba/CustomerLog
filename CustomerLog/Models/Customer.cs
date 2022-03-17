﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerLog.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
