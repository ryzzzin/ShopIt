using System;
using System.Linq;
using System.Threading.Tasks;
using ShopIt.Contexts;
using ShopIt.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopIt.Models.AdminModels;
using System.Collections.Generic;

namespace ShopIt.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
            var addedProducts = _context.Cart.Where(x => x.UserId == order.UserId && !x.Deleted).ToArray();
            foreach(var item in addedProducts)
            {
                var orderedProduct = new OrderedProduct
                {
                    OrderId = order.Id,
                    ProductId = item.Id,
                    Quantity = item.Quantity,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = User.Identity.Name
                };
                _context.OrderedProducts.Add(orderedProduct);
            }
            _context.SaveChanges();
            order.OrderedProducts = _context.OrderedProducts.Where(x => x.OrderId == order.Id).ToList();
            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index", "Orders");
        }


    }
}