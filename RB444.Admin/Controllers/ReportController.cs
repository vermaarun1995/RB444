using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Model.ViewModel;
using RB444.Models.Model;

namespace RB444.Admin.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public ReportController(IRequestServices requestServices, IConfiguration configuration)
        {
            _requestServices = requestServices;
            _configuration = configuration;
        }
        public async Task<IActionResult> RollingCommision()
        {
            var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;
            ViewBag.LoginUser = user;
            CommonReturnResponse commonModel = null;
            List<Model.ViewModel.RollingCommisionVM> rollingCommisionVMs = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}Common/GetRollingCommission?PrentId={1}&UserId=13&Type=2", _configuration["ApiKeyUrl"], user.Id));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    rollingCommisionVMs = jsonParser.ParsJson<List<Model.ViewModel.RollingCommisionVM>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(rollingCommisionVMs);
        }

        public async Task<IActionResult> SettlementData()
        {
            var user = Request.Cookies["loginUserDetail"]!=null? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]):null;
            ViewBag.LoginUser = user;
            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            List<Bets> openBetList = new List<Bets>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSports?type=2", _configuration["ApiKeyUrl"]));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
                }
                ViewBag.SportsList = sportsDatalist;
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(openBetList);
        }

        [HttpPost]
        public async Task<ActionResult> GetBetEventData(int SportId)
        {
            CommonReturnResponse commonModel = null;
            List<MarketVM> eventList = new List<MarketVM>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetEventList?SportId={1}", _configuration["ApiKeyUrl"], SportId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    eventList = jsonParser.ParsJson<List<MarketVM>>(Convert.ToString(commonModel.Data));                   
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return Json(eventList);
        }
        [HttpPost]
        public async Task<ActionResult> GetBetDataList(long EventId)
        {
            CommonReturnResponse commonModel = null;
            List<Bets> openBetList = new List<Bets>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetBetDataList?EventId={1}", _configuration["ApiKeyUrl"], EventId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    openBetList = jsonParser.ParsJson<List<Bets>>(Convert.ToString(commonModel.Data));                    
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return PartialView("_betDataList", openBetList);
        }       
    }
}
