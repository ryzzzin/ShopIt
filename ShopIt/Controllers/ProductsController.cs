using ShopIt.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopIt.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShopIt.Models.AdminModels;
using ShopIt.Models.Entities;

namespace ShopIt.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Where(x => !x.Deleted).AsEnumerable();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Title = model.Title,
                    Price = model.Price,
                    Stock = model.Stock,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = User.Identity.Name
                };                
                _context.Products.Add(product);
                _context.SaveChanges();
                _context.Categories.Where(x => x.Id == product.CategoryId).FirstOrDefault().Products = _context.Products.Where(x => x.CategoryId == product.CategoryId && !x.Deleted).ToList();
                _context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            var model = new EditProductRequest
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                Description = product.Description
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductRequest model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.Find(model.Id);
                if (product is not null)
                {
                    product.Title = model.Title;
                    product.Price = model.Price;
                    product.Stock = model.Stock;
                    product.CategoryId = model.CategoryId;
                    product.Description = model.Description;
                    product.ModifiedAt = DateTime.Now;
                    product.ModifiedBy = User.Identity.Name;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Products");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            product.Deleted = true;
            product.DeletedAt = DateTime.Now;
            product.DeletedBy = User.Identity.Name;
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}
