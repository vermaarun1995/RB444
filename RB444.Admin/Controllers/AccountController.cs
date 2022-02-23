using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RB444.Data.Entities;
using RB444.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }
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
                var user = await _userManager.FindByEmailAsync(model.Email);
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return Redirect("/");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {                
                return View(model);
            }
        }
    }
}
