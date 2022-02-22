using AmazingShop.Models;
using AmazingShop.Repositories;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    [Authorize(Roles = nameof(UserRoles.Admin))]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var users = _userRepository.GetAllAsVM();
            return View(users);
        }
        public IActionResult Promote(string id)
        {
            _userRepository.PromoteToAdmin(id);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Demote(string id)
        {
            _userRepository.DemoteAdmin(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
