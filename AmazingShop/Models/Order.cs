using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models
{
    public class Order
    {
        public OrderHeader Header { get; set; }
        public IEnumerable<OrderedProduct> Products { get; set; }
    }
}
