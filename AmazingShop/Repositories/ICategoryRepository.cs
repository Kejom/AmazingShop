using AmazingShop.Models;
using System.Collections.Generic;

namespace AmazingShop.Repositories
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void RemoveById(int id);
        void UpdateCategory(Category category);
    }
}