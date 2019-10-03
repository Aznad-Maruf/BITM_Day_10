using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmDay8.Model
{
    public class CustomerOrders
    {
        public string CustomerOrderID { get; set; }
        public string CustomerID { get; set; }
        public string ItemID { get; set; }
        public string Quantity { get; set; }
    }
}
