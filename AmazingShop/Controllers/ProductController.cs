using AmazingShop.Models;
using AmazingShop.Models.ViewModels;
using AmazingShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ProductCreateEditVM(new Product(), _categoryRepository.GetAll(), _productRepository.ImagePath);
            return View("CreateEdit", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductCreateEditVM viewModel)
        {
            if(!ModelState.IsValid)
                return View("CreateEdit", viewModel);

            var files = HttpContext.Request.Form.Files;
            _productRepository.AddProduct(viewModel.Product, files[0]);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();
            var product = _productRepository.GetById((int)id);
            var viewModel = new ProductCreateEditVM(product, _categoryRepository.GetAll(), _productRepository.ImagePath);
            return View("CreateEdit", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProductCreateEditVM viewModel)
        {
            if (!ModelState.IsValid)
                return View("CreateEdit", viewModel);

            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
                _productRepository.UpdateProduct(viewModel.Product, files[0]);
            else
                _productRepository.UpdateProduct(viewModel.Product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            _productRepository.RemoveById((int)id);
            return RedirectToAction("Index");

        }
    }
}
