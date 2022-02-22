using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
using System.Collections.Generic;

namespace AmazingShop.Repositories
{
    public interface IUserRepository
    {
        AppUser GetById(string id);
        IEnumerable<UserVM> GetAllAsVM();
        void PromoteToAdmin(string userId);
        void DemoteAdmin(string userId);
    }
}