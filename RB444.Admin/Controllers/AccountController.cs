using Microsoft.AspNetCore.Mvc;

namespace RB444.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }
    }
}
