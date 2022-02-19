using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models.ViewModels
{
    public class CartProductVM
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
