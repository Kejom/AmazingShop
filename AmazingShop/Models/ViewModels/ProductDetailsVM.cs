using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models.ViewModels
{
    public class ProductDetailsVM
    {
        public Product Product { get; set; }
        public bool ExistsInCart { get; set; }
        public string ImagePath { get; set; }
    }
}
