using AmazingShop.Database;
using AmazingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _dbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            IEnumerable<Category> categories = _dbContext.Category;
            return categories;
        }

        public Category GetById(int id)
        {
            return _dbContext.Category.FirstOrDefault(c => c.Id == id);
        }

        public void RemoveById(int id)
        {
            var categoryToRemove = GetById(id);
            if (categoryToRemove is null)
                throw new ArgumentException("Kategoria z podanym Id nie istnieje");

            _dbContext.Category.Remove(categoryToRemove);
            _dbContext.SaveChanges();
        }

        public void AddCategory(Category category)
        {
            if (category is null)
                throw new ArgumentNullException();
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            if (category is null)
                throw new ArgumentNullException();
            _dbContext.Category.Update(category);
            _dbContext.SaveChanges();
        }
    }
}
