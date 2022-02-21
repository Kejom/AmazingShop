using AmazingShop.Database;
using AmazingShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<OrderHeader> GetAllHeaders()
        {
            IEnumerable<OrderHeader> result = _dbContext.OrderHeaders;
            return result;
        }
        public OrderHeader GetHeaderById(int id)
        {
            return _dbContext.OrderHeaders.Include(h=>h.Address).FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<OrderedProduct> GetProductsByOrderId(int id)
        {
            return _dbContext.OrderedProducts.Where(p => p.OrderHeaderId == id).Include(p => p.Product);
        }

        public Order GetById(int id)
        {
            return new Order
            {
                Header = GetHeaderById(id),
                Products = GetProductsByOrderId(id)
            };
        }
        public IEnumerable<OrderHeader> GetHeadersByUserId(string id)
        {
            return _dbContext.OrderHeaders.Where(h => h.CreatedByUserId == id);
        }

        public void AddOrder(OrderHeader header, IEnumerable<OrderedProduct> products)
        {
            _dbContext.OrderHeaders.Add(header);
            _dbContext.SaveChanges();
            foreach (var product in products)
            {
                product.OrderHeaderId = header.Id;
                _dbContext.OrderedProducts.Add(product);
            }
            _dbContext.SaveChanges();
        }
        public void AddProduct(OrderedProduct product, int orderId)
        {
            var order = _dbContext.OrderHeaders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
                throw new ArgumentException("Zamówienie z podanym Id nie istnieje");
            product.OrderHeaderId = orderId;
            _dbContext.OrderedProducts.Add(product);
            _dbContext.SaveChanges();
        }

        public void UpdateHeader(OrderHeader header)
        {
            _dbContext.OrderHeaders.Update(header);
            _dbContext.SaveChanges();
        }
    }
}
