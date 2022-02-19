using AmazingShop.Models;
using System.Collections.Generic;

namespace AmazingShop.Repositories
{
    public interface IAddressRepository
    {
        void Add(Address address);
        Address GetById(int id);
        IEnumerable<Address> GetUserAddresses(string userId);
        void RemoveById(int id);
        void Update(Address address);
    }
}