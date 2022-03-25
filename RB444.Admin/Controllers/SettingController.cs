﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private readonly UserManager<Users> _userManager;

        public SettingController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
        }

        #region SportsSetting
        public async Task<ActionResult> SportsSetting()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
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
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SportsSetting(Sports model)
        {
            try
            {
                var commonModel = await _requestServices.PostAsync<Sports, CommonReturnResponse>(string.Format("{0}Setting/AddOrUpdateSportsSetting", _configuration["ApiKeyUrl"]), model);
                var data = JsonConvert.SerializeObject(commonModel);
                return Json(data);
            }
            catch (Exception ex)
            {
                var data = new CommonReturnResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return Json(JsonConvert.SerializeObject(data));
            }
        }
        #endregion

        #region SeriesSetting
        public async Task<ActionResult> SeriesSetting()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            List<Series> serieslist = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSports?type=2", _configuration["ApiKeyUrl"]));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
                }
                ViewBag.SportsList = sportsDatalist;

                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSeries?SportId=4&type=2", _configuration["ApiKeyUrl"]));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    serieslist = jsonParser.ParsJson<List<Series>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(serieslist);
        }

        [HttpPost]
        public async Task<ActionResult> FilterSeries(int status, int SportId)
        {
            CommonReturnResponse commonModel = null;
            List<Series> serieslist = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSeries?SportId={1}&type=2", _configuration["ApiKeyUrl"], SportId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    serieslist = jsonParser.ParsJson<List<Series>>(Convert.ToString(commonModel.Data));
                    if (status > 0)
                    {
                        serieslist = serieslist.Where(s => s.Status == Convert.ToBoolean(status - 1)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return PartialView("_serieslist", serieslist);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatedSeries(Series model)
        {
            CommonReturnResponse commonModel = null;
            try
            {
                commonModel = await _requestServices.PostAsync<Series, CommonReturnResponse>(String.Format("{0}setting/UpdateSeriesSetting", _configuration["ApiKeyUrl"]), model);
                var data = JsonConvert.SerializeObject(commonModel);
                return Json(data);

            }
            catch (Exception ex)
            {
                var data = new CommonReturnResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return Json(JsonConvert.SerializeObject(data));
            }

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
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
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
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
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
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

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
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

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
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

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
