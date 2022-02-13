using AmazingShop.Repositories;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartSessionManager _cartManager;
        private readonly IProductRepository _productRepository;

        public CartController(ICartSessionManager cartSessionManager, IProductRepository productReposistory)
        {
            _cartManager = cartSessionManager;
            _productRepository = productReposistory;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
