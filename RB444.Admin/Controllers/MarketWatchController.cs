using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Model.Model;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class MarketWatchController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MarketWatchController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: MarketWatch       
        public ActionResult MarketWatch()
        {
           var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;if (user != null) { ViewBag.LoginUser = user; }else { return RedirectToAction("Login", "Account"); } 
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> TeamMarketWatch(int SportId)
        {
           var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;if (user != null) { ViewBag.LoginUser = user; }else { return RedirectToAction("Login", "Account"); } 
            CommonReturnResponse commonModel = null;
            List<MarketWatchVM> marketWatchVM = new List<MarketWatchVM>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}MarketWatch/GetBetHistory?SportId={1}&UserId={2}", _configuration["ApiKeyUrl"], SportId, 13));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    marketWatchVM = jsonParser.ParsJson<List<MarketWatchVM>>(Convert.ToString(commonModel.Data));
                }
                ViewBag.MarketWatchVM = marketWatchVM;
            }
            catch (Exception ex)
            {
                throw;
            }
          
            return View();
        }

        public ActionResult ManageSeries()
        {
           var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;if (user != null) { ViewBag.LoginUser = user; }else { return RedirectToAction("Login", "Account"); } 
            return View();
        }


        public ActionResult IndianFancy()
        {
           var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;if (user != null) { ViewBag.LoginUser = user; }else { return RedirectToAction("Login", "Account"); }  
            return View();
        }


        public ActionResult SessionFancy()
        {
           var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;if (user != null) { ViewBag.LoginUser = user; }else { return RedirectToAction("Login", "Account"); } 
            return View();
        }


        public ActionResult BetfairMarket()
        {
           var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;if (user != null) { ViewBag.LoginUser = user; }else { return RedirectToAction("Login", "Account"); } 
            return View();
        }
    }
}
