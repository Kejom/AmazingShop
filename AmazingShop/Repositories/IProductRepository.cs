using AmazingShop.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AmazingShop.Repositories
{
    public interface IProductRepository
    {
        string ImagePath { get;}

        void AddProduct(Product product, IFormFile productImage);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void RemoveById(int id);
        void UpdateProduct(Product product);
        void UpdateProduct(Product product, IFormFile productImage);
    }
}