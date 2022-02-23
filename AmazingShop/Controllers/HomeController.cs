using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
using AmazingShop.Repositories;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartSessionManager _cartManager;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository, IProductRepository productRepository, ICartSessionManager cartSessionManager)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _cartManager = cartSessionManager;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            var products = _productRepository.GetAll().Where(p=>!p.Hidden).Select(p => AddFullImageLinkToProduct(p));
            var viewModel = new HomeVM
            {
                Categories = categories,
                Products = products
            };

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null)
                return NotFound();

            var viewModel = new ProductDetailsVM
            {
                Product = product,
                ExistsInCart = _cartManager.IsProductInCart(product.Id),
                ImagePath = _productRepository.ImagePath
            };

            return View(viewModel);
        }

        public IActionResult AddToCart(int id)
        {
            _cartManager.AddProductToCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartManager.RemoveProductFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Product AddFullImageLinkToProduct(Product product)
        {
            var imagePath = _productRepository.ImagePath;
            product.Image = imagePath + product.Image;
            return product;
        }
    }
}
