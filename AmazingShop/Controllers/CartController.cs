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
        private readonly IOrderRepository _orderRepository;

        public CartController(ICartSessionManager cartSessionManager,
            IProductRepository productReposistory,
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IOrderRepository orderRepository)
        {
            _cartManager = cartSessionManager;
            _productRepository = productReposistory;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
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
            var paymentMethods = GetPaymentMethods();
            var viewModel = new NewOrderVM
            {
                User = user,
                Addresses = addresses.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Country}, {a.City}, {a.Street}" }),
                PaymentMethods = paymentMethods,
                Products = productsInCart,
                TotalPrice = GetTotalPrice(productsInCart)
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Order(NewOrderVM viewModel)
        {
            var products = _cartManager.GetCart();
            var header = new OrderHeader
            {
                FullName = $"{viewModel.User.FirstName} {viewModel.User.Surname}",
                Email = viewModel.User.Email,
                PhoneNumber = viewModel.User.PhoneNumber,
                AddressId = viewModel.SelectedAddressId,
                PayymentMethod = viewModel.SelectedPaymentMethod,
                CreatedByUserId = viewModel.User.Id,
                OrderDate = DateTime.Now,
                OrderStatus = ((int)OrderStatuses.Created),
                FinalOrderTotal = viewModel.TotalPrice
            };
            var orderedProducts = products.Select(p => new OrderedProduct { ProductId = p.ProductId, Quantity = p.Quantity });
            _orderRepository.AddOrder(header, orderedProducts);
            _cartManager.SetCart(new List<ProductInCart>());
            return View("Confirmation", header);
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
        private double GetTotalPrice(IEnumerable<CartProductVM> products)
        {
            double total = 0;
            foreach (var p in products)
                total += (p.Product.Price * p.Quantity);
            return Math.Round(total, 2);
        }

        private IEnumerable<SelectListItem> GetPaymentMethods()
        {
            var methods = Enum.GetValues(typeof(PaymentMethods)).Cast<PaymentMethods>();
            return methods.Select(m => new SelectListItem { Text = m.GetDisplayName(), Value = ((int)m).ToString() });
        }
    }
}
