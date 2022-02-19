using AmazingShop.Models;
using AmazingShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _addressRepository.GetUserAddresses(userId);
            return View(addresses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var address = new Address();
            address.UserId = userId;
            return View("CreateEdit", address);
        }
        [HttpPost]
        public IActionResult Create(Address address)
        {
            if (!ModelState.IsValid)
                return View("CreateEdit", address);

            _addressRepository.Add(address);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();
            var address = _addressRepository.GetById((int)id);
            return View("CreateEdit", address);
        }
        [HttpPost]
        public IActionResult Edit(Address address)
        {
            if (!ModelState.IsValid)
                return View("CreateEdit", address);

            _addressRepository.Update(address);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();
            _addressRepository.RemoveById((int)id);
            return RedirectToAction("Index");
        }
    }
}
