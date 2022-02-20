using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
using AmazingShop.Repositories;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartSessionManager _cartManager;
        private readonly IProductRepository _productRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;

        public CartController(ICartSessionManager cartSessionManager, IProductRepository productReposistory, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _cartManager = cartSessionManager;
            _productRepository = productReposistory;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }
        public IActionResult Index()
        {
            var products = GetFullProductsInCart();
            return View(products);
        }
        public IActionResult Remove(int id)
        {
            _cartManager.RemoveProductFromCart(id);
            return RedirectToAction("Index");
        }
        public IActionResult AddOne(int id)
        {
            _cartManager.IncreaseProductQuantity(id, 1);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveOne(int id)
        {
            _cartManager.IncreaseProductQuantity(id, -1);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Order()
        {
            var user = GetCurrentUser();
            var addresses = _addressRepository.GetUserAddresses(user.Id);
            var productsInCart = GetFullProductsInCart();
            var viewModel = new NewOrderVM
            {
                User = user,
                Addresses = addresses.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Country}, {a.City}, {a.Street}" }),
                Products = productsInCart
            };
            return View(viewModel);
        }

        private IEnumerable<CartProductVM> GetFullProductsInCart()
        {
            var cart = _cartManager.GetCart();
            var products = _productRepository.GetAll();
            var result = cart.Select(p => new CartProductVM
            {
                Product = products.FirstOrDefault(n => n.Id == p.ProductId),
                Quantity = p.Quantity
            });

            foreach (var product in result)
                AddFullImageLinkToProduct(product.Product);

            return result;
        }
        private Product AddFullImageLinkToProduct(Product product)
        {
            var imagePath = _productRepository.ImagePath;
            product.Image = imagePath + product.Image;
            return product;
        }
        private AppUser GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userRepository.GetById(userId);
        }
    }
}
