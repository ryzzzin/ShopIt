using System;
using System.Linq;
using System.Threading.Tasks;
using ShopIt.Contexts;
using ShopIt.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopIt.Models.AdminModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ShopIt.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            Guid userId = user.Id;
            var cart = _context.Cart.Where(x => x.UserId == user.Id && !x.Deleted).ToList();
            foreach(var item in cart)
            {
                item.Product = _context.Products.Where(x => x.Id == item.ProductId).SingleOrDefault();
            }
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAddedProductRequest model)
        {
            var addedProduct = new AddedProduct
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                ModifiedAt = DateTime.Now,
                ModifiedBy = User.Identity.Name
            };
            _context.Cart.Add(addedProduct);
            _context.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new CreateAddedProductRequest
            {
                UserId = user.Id,
                ProductId = productId,
                Quantity = quantity
            };
            return await Create(model);
        }

            [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var addedProduct = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (addedProduct is null)
            {
                return NotFound();
            }
            var model = new EditCategoryRequest
            {
                Id = addedProduct.Id,
                Title = addedProduct.Title
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAddedProductRequest model)
        {
            if (ModelState.IsValid)
            {
                var addedProduct = _context.Cart.Find(model.Id);
                if (addedProduct is not null)
                {
                    addedProduct.ProductId = model.ProductId;
                    addedProduct.Quantity = model.Quantity;
                    addedProduct.ModifiedAt = DateTime.Now;
                    addedProduct.ModifiedBy = User.Identity.Name;
                    _context.Cart.Update(addedProduct);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Cart");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var addedProduct = _context.Cart.SingleOrDefault(x => x.Id == id);
            if (addedProduct is null)
            {
                return NotFound();
            }
            addedProduct.Deleted = true;
            addedProduct.DeletedAt = DateTime.Now;
            addedProduct.DeletedBy = User.Identity.Name;
            _context.Cart.Update(addedProduct);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

    }
}