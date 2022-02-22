using Microsoft.AspNetCore.Mvc;
using RB444.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
               
                return View(model);
            }
            catch (Exception ex)
            {                
                return View(model);
            }
        }
    }
}
