using AmazingShop.Database;
using AmazingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private AppDbContext _dbContext;
        public AddressRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public IEnumerable<Address> GetUserAddresses(string userId)
        {
            return _dbContext.Addresses.Where(a => a.UserId == userId);
        }
        public Address GetById(int id)
        {
            return _dbContext.Addresses.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Address address)
        {
            if (address is null)
                throw new ArgumentNullException();

            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
        }
        public void Update(Address address)
        {
            if (address is null)
                throw new ArgumentNullException();

            _dbContext.Addresses.Update(address);
            _dbContext.SaveChanges();
        }
        public void RemoveById(int id)
        {
            var addressToRemove = GetById(id);
            if (addressToRemove is null)
                throw new ArgumentException("Adres z podanym Id nie istnieje");

            _dbContext.Addresses.Remove(addressToRemove);
            _dbContext.SaveChanges();
        }
    }
}
