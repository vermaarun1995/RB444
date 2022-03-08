using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RB444.Data.Entities;

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
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            return View();
        }

        [HttpGet]
        public ActionResult TeamMarketWatch(int id)
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            return View();
        }

        public ActionResult ManageSeries()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            return View();
        }


        public ActionResult IndianFancy()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            return View();
        }


        public ActionResult SessionFancy()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            return View();
        }


        public ActionResult BetfairMarket()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            return View();
        }
    }
}
