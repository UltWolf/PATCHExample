using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PATCH.Models
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public List<Order> Orders { get; set; }
    }
    public class Order
    {
        public string OrderName { get; set; }
        public string OrderType { get; set; }
    }
}
