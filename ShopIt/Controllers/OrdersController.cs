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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = _context.Orders.Where(x => x.UserId == user.Id && !x.Deleted).ToList();
            var statuses = _context.Statuses.Where(x => !x.Deleted).ToList();
            foreach (var item in orders)
            {
                item.Status = statuses.Where(x => x.Id == item.StatusId).SingleOrDefault();
                item.OrderedProducts = _context.OrderedProducts.Where(x => x.OrderId == item.Id).ToList();
            }

            return View(orders);
        }

        public async Task<IActionResult> GetAll()
        {
            var orders = _context.Orders.AsEnumerable();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest model)
        {
            var order = new Order
            {
                UserId = model.UserId,
                StatusId = model.StatusId,
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                ModifiedAt = DateTime.Now,
                ModifiedBy = User.Identity.Name
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            var addedProducts = _context.Cart.Where(x => x.UserId == order.UserId && !x.Deleted).ToArray();
            foreach(var item in addedProducts)
            {
                var orderedProduct = new OrderedProduct
                {
                    OrderId = order.Id,
                    ProductId = _context.Products.Where(x => x.Id == item.ProductId && !x.Deleted).FirstOrDefault().Id,
                    Quantity = item.Quantity,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = User.Identity.Name
                };
                _context.OrderedProducts.Add(orderedProduct);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Orders");
        }

        public async Task<IActionResult> Order()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new CreateOrderRequest
            {
                UserId = user.Id,
                StatusId = 1,
            };
            return await Create(model);
        }
    }
}