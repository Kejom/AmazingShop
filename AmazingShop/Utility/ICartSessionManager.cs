using AmazingShop.Models;
using System.Collections.Generic;

namespace AmazingShop.Utility
{
    public interface ICartSessionManager
    {
        void AddProductToCart(int productId);
        List<ProductInCart> GetCart();
        void RemoveProductFromCart(int productId);
        void SetCart(List<ProductInCart> cart);
        bool IsProductInCart(int productId);
    }
}