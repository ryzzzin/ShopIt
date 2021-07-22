using System.Net.Mime;
using System.Threading.Tasks;
using ShopIt.Models;
using ShopIt.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ShopIt.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(ILogger<AccountsController> logger, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            _logger.LogInformation("Login view open");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            _logger.LogInformation("[POST] Login method start");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user is null)
                {
                    ModelState.AddModelError("", "User with this email not found");
                    return View(login);
                }

                var result = await _userManager.CheckPasswordAsync(user, login.Password);
                if (result)
                {
                    await _signInManager.SignInAsync(user, login.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "Password is invalid");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            _logger.LogInformation("Register view open");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterRequest register)
        {
            _logger.LogInformation("[POST] Register method start");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = register.Email,
                    UserName = register.UserName,
                };
                
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}