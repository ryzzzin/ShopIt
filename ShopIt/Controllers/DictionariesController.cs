using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShopIt.Contexts;
using ShopIt.Models.Entities;
using ShopIt.Models.AdminModels;
using Microsoft.AspNetCore.Authorization;

namespace ShopIt.Controllers
{
    public class DictionariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DictionariesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Statuses

        [HttpGet]
        public async Task<IActionResult> DisplayStatusList()
        {
            return View(_context.Statuses.Where(x => !x.Deleted).AsEnumerable());
        }

        [HttpGet]
        public async Task<IActionResult> CreateStatus()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(CreateStatusRequest model)
        {
            if (ModelState.IsValid)
            {
                var status = new Status
                {
                    Title = model.Title,
                    Icon = model.Icon,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = User.Identity.Name
                };
                _context.Statuses.Add(status);
                _context.SaveChanges();
                return RedirectToAction("DisplayStatusList", "Dictionaries");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditStatus(int id)
        {
            var status = _context.Statuses.SingleOrDefault(x => x.Id == id);
            if (status is null)
            {
                return NotFound();
            }
            var model = new EditStatusRequest
            {
                Id = status.Id,
                Title = status.Title,
                Icon = status.Icon,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(EditStatusRequest model)
        {
            if (ModelState.IsValid)
            {
                var status = _context.Statuses.Find(model.Id);
                if (status is not null)
                {
                    status.Title = model.Title;
                    status.Icon = model.Icon;
                    status.ModifiedAt = DateTime.Now;
                    status.ModifiedBy = User.Identity.Name;
                    _context.Statuses.Update(status);
                    _context.SaveChanges();
                    return RedirectToAction("DisplayStatusList", "Dictionaries");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = _context.Statuses.SingleOrDefault(x => x.Id == id);
            if (status is null)
            {
                return NotFound();
            }
            status.Deleted = true;
            status.DeletedAt = DateTime.Now;
            status.DeletedBy = User.Identity.Name;
            _context.Statuses.Update(status);
            _context.SaveChanges();
            return RedirectToAction("DisplayStatusList", "Dictionaries");
        }

        #endregion
    }
}
