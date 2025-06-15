using Microsoft.AspNetCore.Mvc;

namespace Task3.Controllers
{
    public class AuthenticatedController : Controller
    {
        protected bool IsLoggedIn => HttpContext.Session.GetString("username") != null;

        protected IActionResult RequireLoginRedirect()
        {
            if (!IsLoggedIn)
                return RedirectToAction("Login", "Account");
            return null;
        }
    }
}
