using AmazingShop.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Utility
{
    public class CartSessionManager : ICartSessionManager
    {
        private readonly string _sessionName = "ShoppingCartSession";
        private readonly IHttpContextAccessor _context;

        public CartSessionManager(IHttpContextAccessor context)
        {
            _context = context;
        }

        public List<ProductInCart> GetCart()
        {
            var cart = _context.HttpContext.Session.Get<List<ProductInCart>>(_sessionName);

            if (cart is null)
                cart = new List<ProductInCart>();

            return cart;
        }

        public void SetCart(List<ProductInCart> cart)
        {
            _context.HttpContext.Session.Set(_sessionName, cart);
        }

        public void AddProductToCart(int productId)
        {
            var cart = GetCart();
            cart.Add(new ProductInCart { ProductId = productId });
            SetCart(cart);
        }

        public void RemoveProductFromCart(int productId)
        {
            var cart = GetCart();
            cart = cart.Where(p => p.ProductId != productId).ToList();
            SetCart(cart);
        }

        public bool IsProductInCart(int productId)
        {
            var cart = GetCart();
            return cart.FirstOrDefault(p => p.ProductId == productId) is not null;
        }
    }
}
