using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models.ViewModels
{
    public class ProductCreateEditVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories;
        public string ImagePath { get; set; }

        public ProductCreateEditVM()
        {

        }
        public ProductCreateEditVM(Product product, IEnumerable<Category> categories, string imagePath)
        {
            Product = product;
            Categories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            ImagePath = imagePath;
        }
    }
}
