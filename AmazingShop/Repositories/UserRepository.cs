using AmazingShop.Database;
using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public AppUser GetById(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }
        public IEnumerable<UserVM> GetAllAsVM()
        {
            IEnumerable<UserVM> result = _userManager.Users.Select(u => new UserVM
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.Surname}",
                Email = u.Email,
                IsAdmin = _userManager.IsInRoleAsync(u, nameof(UserRoles.Admin)).GetAwaiter().GetResult(),
                IsMasterAdmin = _userManager.IsInRoleAsync(u, nameof(UserRoles.MasterAdmin)).GetAwaiter().GetResult()
        });
            return result;
        }

        public void PromoteToAdmin(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
                throw new ArgumentException("Użytkownik z podanym Id nie istnieje");

            if (_userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()).GetAwaiter().GetResult())
                return;

            _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString()).GetAwaiter().GetResult();
        }

        public void DemoteAdmin(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null)
                throw new ArgumentException("Użytkownik z podanym Id nie istnieje");

            if (_userManager.IsInRoleAsync(user, UserRoles.MasterAdmin.ToString()).GetAwaiter().GetResult())
                throw new InvalidOperationException("Nie można modyfikowac konta Master Admin");

            if (!_userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()).GetAwaiter().GetResult())
                return;

            _userManager.RemoveFromRoleAsync(user, UserRoles.Admin.ToString()).GetAwaiter().GetResult();
        }
    }
}
