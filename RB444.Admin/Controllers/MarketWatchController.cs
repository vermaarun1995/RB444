using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class MarketWatchController : Controller
    {
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
