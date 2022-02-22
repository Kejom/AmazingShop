using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
using AmazingShop.Repositories;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Authorization;
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
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index(string searchName = null, string searchEmail = null, string searchPhone = null, string Status = null, bool admin = false)
        {
            var viewModel = new OrderListVM();
            viewModel.IsAdmin = admin;
            if(!admin || !User.IsInRole(UserRoles.Admin.ToString()))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                viewModel.Orders = _orderRepository.GetHeadersByUserId(userId);
                return View(viewModel);
            }
            var headers = _orderRepository.GetAllHeaders();

            if (!string.IsNullOrEmpty(searchName))
            {
                headers = headers.Where(u => u.FullName.ToLower().Contains(searchName.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchEmail))
            {
                headers = headers.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchPhone))
            {
                headers = headers.Where(u => u.PhoneNumber.ToLower().Contains(searchPhone.ToLower()));
            }
            if (!string.IsNullOrEmpty(Status) && Status != "--Order Status--")
            {
                headers = headers.Where(u => u.OrderStatus == Int32.Parse(Status));
            }
            viewModel.Orders = headers;
            viewModel.Statuses = GetStatuses();
            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var isAdmin = User.IsInRole(UserRoles.Admin.ToString());
            var order = _orderRepository.GetById(id);

            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
                return View(order);
            }

            ViewBag.IsAdmin = false;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (order.Header.CreatedByUserId != userId)
                return Unauthorized();

            return View(order);
        }

        [HttpPost]
        public IActionResult StartProcessing(Order order)
        {
            OrderHeader orderHeader = _orderRepository.GetHeaderById(order.Header.Id);
            orderHeader.OrderStatus = ((int)OrderStatuses.Processed);
            _orderRepository.UpdateHeader(orderHeader);
            return RedirectToAction(nameof(Index), new { admin = true });
        }

        [HttpPost]
        public IActionResult ShipOrder(Order order)
        {
            OrderHeader orderHeader = _orderRepository.GetHeaderById(order.Header.Id);
            orderHeader.OrderStatus = ((int)OrderStatuses.Shipped);
            orderHeader.ShippingDate = DateTime.Now;
            _orderRepository.UpdateHeader(orderHeader);
            return RedirectToAction(nameof(Index), new { admin = true });
        }

        [HttpPost]
        public IActionResult CancelOrder(Order order)
        {
            OrderHeader orderHeader = _orderRepository.GetHeaderById(order.Header.Id);
            orderHeader.OrderStatus = ((int)OrderStatuses.Canceled);
            _orderRepository.UpdateHeader(orderHeader);
            return RedirectToAction(nameof(Index),new {admin = true });
        }

        [HttpPost]
        public IActionResult UpdateOrderDetails(Order order)
        {
            if (!ModelState.IsValid)
                return View("Details", order);
            OrderHeader orderHeaderFromDb = _orderRepository.GetHeaderById(order.Header.Id);

            orderHeaderFromDb.FullName = order.Header.FullName;
            orderHeaderFromDb.PhoneNumber = order.Header.PhoneNumber;
            orderHeaderFromDb.Email = order.Header.Email;
            orderHeaderFromDb.City = order.Header.City;
            orderHeaderFromDb.Country = order.Header.Country;
            orderHeaderFromDb.Street = order.Header.Street;
            orderHeaderFromDb.BuildingNumber = order.Header.BuildingNumber;
            orderHeaderFromDb.LocalNumber = order.Header.LocalNumber;
            orderHeaderFromDb.ZipPostalCode = order.Header.ZipPostalCode;

            _orderRepository.UpdateHeader(orderHeaderFromDb);
            return RedirectToAction("Details", "Order", new { id = orderHeaderFromDb.Id });
        }

        private IEnumerable<SelectListItem> GetStatuses()
        {
            var methods = Enum.GetValues(typeof(OrderStatuses)).Cast<OrderStatuses>();
            return methods.Select(m => new SelectListItem { Text = m.GetDisplayName(), Value = ((int)m).ToString() });
        }


    }
}
