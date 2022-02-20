using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
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
        private readonly IAddressRepository _addressRepository;
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
        public IActionResult Create(string ctr = null, string act = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var address = new Address();
            address.UserId = userId;
            var viewModel = new VMWithRedirect<Address>
            {
                Model = address,
                Controller = ctr,
                Action = act
            };
            return View("CreateEdit", viewModel);
        }
        [HttpPost]
        public IActionResult Create(VMWithRedirect<Address> viewModel)
        {
            if (!ModelState.IsValid)
                return View("CreateEdit", viewModel);

            _addressRepository.Add(viewModel.Model);
            if (!String.IsNullOrEmpty(viewModel.Controller)  && !String.IsNullOrEmpty(viewModel.Action))
                return RedirectToAction(viewModel.Action, viewModel.Controller);
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
