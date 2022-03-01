
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class SettingController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public SettingController(IRequestServices requestServices, IConfiguration configuration)
        {
            _requestServices = requestServices;
            _configuration = configuration;
        }

        #region SportsSetting
        public async Task<ActionResult> SportsSetting()
        {
            CommonReturnResponse commonModel = null;
            List<SportsData> sportsDatalist = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}BetfairApi/BetfairApi/GetSportsList", _configuration["ApiKeyUrl"]));
                sportsDatalist = jsonParser.ParsJson<List<SportsData>>(Convert.ToString(commonModel.Data));
                ViewBag.SportsList = sportsDatalist;
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View();
        }

        public ActionResult CreateSports()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSports(SportsSetting obj)
        {
            try
            {
                bool isName = obj.SportName != null && obj.SportName.Trim() != null ? true : false;
                if (isName)
                {
                    //SuperAdminBAL _bal = new SuperAdminBAL();
                    //bool isUpdated = _bal.PostSportesSettings(obj);
                    return RedirectToAction("SportsSetting", "Setting");
                }
                else
                {
                    TempData["ErrorMsg"] = "Please Fill Sports Name.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SeriesSetting
        public async Task<ActionResult> SeriesSetting()
        {
            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            List<Series> serieslist = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}BetfairApi/BetfairApi/GetSportsList", _configuration["ApiKeyUrl"]));
                sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
                ViewBag.SportsList = sportsDatalist;

                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}BetfairApi/BetfairApi/GetSeriesList", _configuration["ApiKeyUrl"]));
                serieslist = jsonParser.ParsJson<List<Series>>(Convert.ToString(commonModel.Data));
                ViewBag.SeriesList = serieslist;
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View();
        }

        public async Task<ActionResult> CreateSeries()
        {
            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}BetfairApi/BetfairApi/GetSportsList", _configuration["ApiKeyUrl"]));
            sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
            ViewBag.SportsList = sportsDatalist;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSeries(SeriesSetting obj)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
        #endregion

        #region match setting
        public async Task<ActionResult> MatchSettings()
        {
            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}BetfairApi/BetfairApi/GetSportsList", _configuration["ApiKeyUrl"]));
                sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
                ViewBag.SportsList = sportsDatalist;
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View();
        }

        public ActionResult CreateMatchSetting()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMatchSetting(MatchSetting obj)
        {
            return View();
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> SliderSetting()
        {
            CommonReturnResponse commonModel = null;
            List<Slider> sliderList = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllSliders", _configuration["ApiKeyUrl"]));
                sliderList = jsonParser.ParsJson<List<Slider>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(sliderList);
        }

        [HttpGet]
        public async Task<ActionResult> LogoSetting()
        {
            CommonReturnResponse commonModel = null;
            List<Logo> logoList = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllLogo", _configuration["ApiKeyUrl"]));
                logoList = jsonParser.ParsJson<List<Logo>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(logoList);
        }

        [HttpGet]
        public async Task<ActionResult> NewsSetting()
        {
            CommonReturnResponse commonModel = null;
            List<News> newsList = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllNews", _configuration["ApiKeyUrl"]));
                newsList = jsonParser.ParsJson<List<News>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(newsList);
        }
    }
}
