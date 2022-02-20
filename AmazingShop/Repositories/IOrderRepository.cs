using AmazingShop.Models;
using System.Collections.Generic;

namespace AmazingShop.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderHeader> GetAllHeaders();
        Order GetById(int id);
        OrderHeader GetHeaderById(int id);
        IEnumerable<OrderHeader> GetHeadersByUserId(string id);
        IEnumerable<OrderedProduct> GetProductsByOrderId(int id);
        void AddOrder(OrderHeader header, IEnumerable<OrderedProduct> products);
        public void AddProduct(OrderedProduct product, int orderId);

    }
}