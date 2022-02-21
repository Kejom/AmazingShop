using AmazingShop.Models;
using AmazingShop.Repositories;
using AmazingShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    [Authorize(Roles = nameof(UserRoles.Admin))]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll().OrderBy(c => c.DisplayOrder);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryRepository.AddCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var category = _categoryRepository.GetById((int)id);

            if (category is null)
                return NotFound();

            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryRepository.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            _categoryRepository.RemoveById((int)id);
            return RedirectToAction("Index");

        }
    }
}
