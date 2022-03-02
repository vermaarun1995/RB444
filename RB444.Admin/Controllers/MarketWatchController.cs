using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RB444.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class MarketWatchController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public MarketWatchController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }
        // GET: MarketWatch       
        public async Task<ActionResult> MarketWatch()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> TeamMarketWatch(int id)
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }

        public async Task<ActionResult> ManageSeries()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }


        public async Task<ActionResult> IndianFancy()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }


        public async Task<ActionResult> SessionFancy()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }


        public async Task<ActionResult> BetfairMarket()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }
    }
}
