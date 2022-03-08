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
        public ActionResult MarketWatch()
        {           
            return View();
        }

        [HttpGet]
        public ActionResult TeamMarketWatch(int id)
        {
            return View();
        }

        public ActionResult ManageSeries()
        {
            return View();
        }


        public ActionResult IndianFancy()
        {
            return View();
        }


        public ActionResult SessionFancy()
        {
            return View();
        }


        public ActionResult BetfairMarket()
        {
            return View();
        }
    }
}
