using AmazingShop.Models;

namespace AmazingShop.Repositories
{
    public interface IUserRepository
    {
        AppUser GetById(string id);
    }
}