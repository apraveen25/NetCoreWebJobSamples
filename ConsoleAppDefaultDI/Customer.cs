using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppDefaultDI
{
    [Serializable]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSales { get; set; }
    }
}
