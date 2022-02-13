using AmazingShop.Database;
using AmazingShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext _dbContext;
        private const string _imagePath = @"\images\product\";
        private readonly string _fullImagePath;


        public string ImagePath
        {
            get { return _imagePath; }
        }

        public ProductRepository(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _fullImagePath = webHostEnvironment.WebRootPath + _imagePath;
        }

        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> products = _dbContext.Products.Include(p => p.Category);
            return products;
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void RemoveById(int id)
        {
            var productToRemove = GetById(id);
            if (productToRemove is null)
                throw new ArgumentException("Product z podanym Id nie istnieje");

            _dbContext.Products.Remove(productToRemove);
            _dbContext.SaveChanges();
        }

        public void AddProduct(Product product, IFormFile productImage)
        {
            if (product is null || productImage is null)
                throw new ArgumentNullException();

            product.Image = SaveProductImage(productImage);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            if (product is null)
                throw new ArgumentNullException();

            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product, IFormFile productImage)
        {
            if (product is null || productImage is null)
                throw new ArgumentNullException();

            RemoveProductImage(product.Image);
            product.Image = SaveProductImage(productImage);
            UpdateProduct(product);
        }


        private string SaveProductImage(IFormFile productImage)
        {
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(productImage.FileName);
            string fullFileName = fileName + extension;

            using (var fileStream = new FileStream(Path.Combine(_fullImagePath, fullFileName), FileMode.Create))
            {
                productImage.CopyTo(fileStream);
            }
            return fullFileName;
        }

        private void RemoveProductImage(string image)
        {
            var fullPath = Path.Combine(_fullImagePath, image);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
