using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopIt.Contexts;
using ShopIt.Models.AdminModels;
using ShopIt.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ShopIt.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string _path;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _path= Path.Combine("~", "Images");
        }
        
        public async Task<IActionResult> Index(ShowImageRequest model)
        {
            var user = await _userManager.GetUserAsync(User);
            var userPic =_context.UserPics.Where(x=>x.UserId==user.Id);
            model = new ShowImageRequest();
            if(userPic.FirstOrDefault() is not null)
            {
                model.Path = userPic.FirstOrDefault().Path;
                return View(model);
            }
            
            model.Path ="Default.png";
            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> EditProfilePicture(IFormFile file)
        {
            if (file != null && file.Length > 0)
                try
                {
                    string fileName = string.Format(@"{0}.png", Guid.NewGuid());
                    string path;
                    path= Path.Combine(_path, fileName);
                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    ViewBag.Message = "File uploaded successfully";
                    var user =await _userManager.GetUserAsync(User);
                    UserPic userPic = new UserPic()
                    {
                        UserId = user.Id,
                        Path = fileName
                    };
                    await _context.UserPics.AddAsync(userPic);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Index");
        }
    }
}
