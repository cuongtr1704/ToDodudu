using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using Task3.Services;

namespace Task3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("username") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.GetByUsername(username);
            if (user != null && _userService.ValidatePassword(user, password))
            {
                HttpContext.Session.SetString("name", user.Name);
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Username not exist or incorrect password!";
            return View();
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("username") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string username, string password)
        {
            var existing = _userService.GetByUsername(username);
            if (existing != null)
            {
                ViewBag.Error = "Username already exist!";
                return View();
            }

            var newUser = new User
            {
                Name = name,
                Username = username,
                Password = password
            };

            _userService.Create(newUser);
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("username", username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
