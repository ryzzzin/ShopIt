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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.Where(x => !x.Deleted).AsEnumerable();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Title = model.Title,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = User.Identity.Name
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            var model = new EditCategoryRequest
            {
                Id = category.Id,
                Title = category.Title
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest model)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.Find(model.Id);
                if (category is not null)
                {
                    category.Title = model.Title;
                    category.ModifiedAt = DateTime.Now;
                    category.ModifiedBy = User.Identity.Name;
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Categories");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            category.Deleted = true;
            category.DeletedAt = DateTime.Now;
            category.DeletedBy = User.Identity.Name;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
    }
}
